using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NerdDinner.Models
{
    public class DinnerRepository
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

        public Dinner GetDinner(int? id)
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

        public void Update(Dinner dinner)
        {
            db.Dinners.Attach(dinner);
            db.Entry(dinner).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}