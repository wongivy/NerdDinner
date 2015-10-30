using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Remoting.Services;
using System.Web.Mvc;

namespace NerdDinner.Models
{
    [PhoneValidator(ErrorMessage = "Phone# does not match country")]
    public partial class Dinner
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int DinnerId { get; set; }

        [Required(ErrorMessage = "Please enter a Dinner Title")]
        [StringLength(20, ErrorMessage = "Title is too long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the Date of the Dinner")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy @ hh:mm tt}")]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Required]
        public string HostedBy { get; set; }

        [Required(ErrorMessage = "Please enter a description of the dinner")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [Display(Name = "Contact Phone #")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Please enter the location of the Dinner")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please select the country of the dinner")]
        public string Country { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public bool IsHostedBy(string username)
        {
            return HostedBy.Equals(username, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsUserRegistered(string userName)
        {
            return Rsvps.Any(r => r.AttendeeName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }

        public virtual ICollection<RSVP> Rsvps { get; set; }
    }
}