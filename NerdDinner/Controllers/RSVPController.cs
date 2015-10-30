using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class RSVPController : Controller
    {
        DinnerRepository dinnerRepository = new DinnerRepository();

        // GET: RSVP
        [HttpPost, Authorize]
        public ActionResult Register(int id)
        {
            var dinner = dinnerRepository.GetDinner(id);
            if (!dinner.IsUserRegistered(User.Identity.Name))
            {
                RSVP rsvp = new RSVP() {AttendeeName = User.Identity.Name};
                dinner.Rsvps.Add(rsvp);
                dinnerRepository.Save();
            }

            return Content("Thanks - we'll see you there!");
        }
    }
}