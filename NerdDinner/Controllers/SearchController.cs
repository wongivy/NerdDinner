﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class JsonDinner
    {
        public int DinnerID { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public int RSVPCount { get; set; }
    }

    public class SearchController : Controller
    {
        DinnerRepository dinnerRepository = new DinnerRepository();

        // GET: Search
        [HttpPost]
        public ActionResult SearchByLocation(float latitude, float longitude)
        {
            var dinners = dinnerRepository.FindByLocation(latitude, longitude);
            var jsonDinners = from dinner in dinners
                              select new JsonDinner
                              {
                                  DinnerID = dinner.DinnerId,
                                  Latitude = dinner.Latitude,
                                  Longitude = dinner.Longitude,
                                  Title = dinner.Title,
                                  Description = dinner.Description,
                                  RSVPCount = dinner.Rsvps.Count
                              };
            var list = jsonDinners.ToList();
            return Json(list);
        }
    }
}