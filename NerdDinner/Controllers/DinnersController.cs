using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Helpers;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        IDinnerRepository dinnerRepository;

        public DinnersController()
        {
            this.dinnerRepository = new DinnerRepository();
        }

        public DinnersController(IDinnerRepository repository)
        {
            dinnerRepository = repository;
        }

        // GET: Dinners
        public ActionResult Index(int? page)
        {
            const int pageSize = 10; 

            var upcomingDinners = dinnerRepository.FindUpcomingDinners();
            var paginatedDinners = new PaginatedList<Dinner>(upcomingDinners, page ?? 0, pageSize);
            return View(paginatedDinners);
        }

        // GET: Dinners/Details/5
        public ActionResult Details(int id)
        {
            var dinner = dinnerRepository.GetDinner(id);
            if (dinner == null)
            {
                return View("NotFound");
            }
            return View("Details", dinner);
        }

        // GET: Dinners/Create
        [Authorize]
        public ActionResult Create()
        {
            var dinner = new Dinner() {EventDate = DateTime.Now.AddDays(7), HostedBy = User.Identity.Name};
            return View(new DinnerFormViewModel(dinner));
        }

        // POST: Dinners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dinner.Rsvps = new List<RSVP>();
                    var rsvp = new RSVP {AttendeeName = User.Identity.Name};
                    dinner.Rsvps.Add(rsvp);

                    dinnerRepository.AddDinner(dinner);
                    dinnerRepository.Save();

                    return RedirectToAction("Details", new {id = dinner.DinnerId});
                }
                catch
                {
                    ModelState.AddRuleViolations(dinner.GetRuleViolations());
                }
            }

            return View(new DinnerFormViewModel(dinner));
        }

        // GET: Dinners/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var dinner = dinnerRepository.GetDinner(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }
            return View(new DinnerFormViewModel(dinner));
        }


        // POST: Dinners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }
            try
            {
                UpdateModel(dinner, "Dinner");
                dinnerRepository.Save();
                return RedirectToAction("Details", new { id = dinner.DinnerId });
            }
            catch
            {
                ModelState.AddRuleViolations(dinner.GetRuleViolations());
                return View(new DinnerFormViewModel(dinner));
            }
        }

        // GET: Dinners/Delete/5
        public ActionResult Delete(int id)
        {
            var dinner = dinnerRepository.GetDinner(id);
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
            var dinner = dinnerRepository.GetDinner(id);
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
