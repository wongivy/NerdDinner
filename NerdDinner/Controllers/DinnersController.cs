using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        private DinnerRepository dinnerRepository = new DinnerRepository();

        // GET: Dinners
        public ActionResult Index()
        {
            var dinners = dinnerRepository.FindUpcomingDinners().ToList();
            return View(dinners);
        }

        // GET: Dinners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = dinnerRepository.GetDinner(id);
            if (dinner == null)
            {
                return View("NotFound");
            }
            return View("Details", dinner);
        }

        // GET: Dinners/Create
        public ActionResult Create()
        {
            Dinner dinner = new Dinner() {EventDate = DateTime.Now.AddDays(7)};
            return View(new DinnerFormViewModel(dinner));
        }

        // POST: Dinners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DinnerId,Title,EventDate,HostedBy,Description,ContactPhone,Address,Country,Latitude,Longitude")] Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                dinnerRepository.AddDinner(dinner);
                dinnerRepository.Save();
                return RedirectToAction("Details", new {id = dinner.DinnerId});
            }

            return View(new DinnerFormViewModel(dinner));
        }

        // GET: Dinners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = dinnerRepository.GetDinner(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            return View(new DinnerFormViewModel(dinner));
        }


        // POST: Dinners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DinnerId,Title,EventDate,HostedBy,Description,ContactPhone,Address,Country,Latitude,Longitude")] Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                dinnerRepository.Update(dinner);
                return RedirectToAction("Details", new {id = dinner.DinnerId});
            }
            return View(new DinnerFormViewModel(dinner));
        }

        // GET: Dinners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = dinnerRepository.GetDinner(id);
            if (dinner == null)
            {
                return View("NotFound");
            }
            return View(dinner);
        }

        // POST: Dinners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);
            dinnerRepository.Delete(dinner);
            dinnerRepository.Save();
            return View("Deleted");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
