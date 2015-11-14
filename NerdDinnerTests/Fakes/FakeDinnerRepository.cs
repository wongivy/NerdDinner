﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdDinner.Models;

namespace NerdDinnerTests.Fakes
{
    public class FakeDinnerRepository: IDinnerRepository
    {
        private List<Dinner> dinnerList;

        public FakeDinnerRepository(List<Dinner> dinners)
        {
            dinnerList = dinners;
        }

        public IQueryable<Dinner> FindAllDinners()
        {
            return dinnerList.AsQueryable();
        }

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude)
        {
            return (from dinner in dinnerList
                where dinner.Latitude == latitude && dinner.Longitude == longitude
                select dinner).AsQueryable();
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return (from dinner in dinnerList
                    where dinner.EventDate > DateTime.Now
                    select dinner).AsQueryable();
        }

        public Dinner GetDinner(int id)
        {
            return dinnerList.SingleOrDefault(d => d.DinnerId == id);
        }

        public void AddDinner(Dinner dinner)
        {
            dinnerList.Add(dinner);
        }

        public void Delete(Dinner dinner)
        {
            dinnerList.Remove(dinner);
        }

        public void Save()
        {
            foreach (Dinner dinner in dinnerList)
            {
                if (!dinner.IsValid)
                {
                    throw new ApplicationException("Rule violations");
                }
            }
        }
    }
}
