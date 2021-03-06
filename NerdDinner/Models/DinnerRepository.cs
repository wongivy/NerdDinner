﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NerdDinner.Models
{
    public class DinnerRepository : IDinnerRepository
    {
        private NerdDinnersDataContext db = new NerdDinnersDataContext();

        public IQueryable<Dinner> FindAllDinners()
        {
            return db.Dinners;
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return from dinner in db.Dinners
                    where dinner.EventDate > DateTime.Now
                    orderby dinner.EventDate
                    select dinner;
        }

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude)
        {
            var dinners = from dinner in FindUpcomingDinners()
                join i in db.NearestDinners(latitude, longitude) 
                on dinner.DinnerId equals i.DinnerId
                select dinner;
            return dinners;
        }

        public Dinner GetDinner(int id)
        {
            return db.Dinners.SingleOrDefault(d => d.DinnerId == id);
        }

        public void AddDinner(Dinner dinner)
        {
            db.Dinners.Add(dinner);
        }

        public void Delete(Dinner dinner)
        {
            db.Rsvps.RemoveRange(dinner.Rsvps);
            db.Dinners.Remove(dinner);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}