using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Runtime.Remoting.Services;
using System.Web.Mvc;

namespace NerdDinner.Models
{
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

        public string HostedBy { get; set; }

        [Required(ErrorMessage = "Please enter a description of the dinner")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [Display(Name = "Contact Phone #")]
        [Phone]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Please enter the location of the Dinner")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please select the country of the dinner")]
        public string Country { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public bool IsHostedBy(string username)
        {
            return HostedBy.Equals(username, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsUserRegistered(string userName)
        {
            return Rsvps.Any(r => r.AttendeeName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Title))
                yield return new RuleViolation("Title required", "Title");

            if (String.IsNullOrEmpty(Description))
                yield return new RuleViolation("Description required", "Description");

            if (String.IsNullOrEmpty(HostedBy))
                yield return new RuleViolation("HostedBy required", "HostedBy");

            if (String.IsNullOrEmpty(Address))
                yield return new RuleViolation("Address required", "Address");

            if (String.IsNullOrEmpty(Country))
                yield return new RuleViolation("Country required", "Country");

            if (String.IsNullOrEmpty(ContactPhone))
                yield return new RuleViolation("Phone# required", "ContactPhone");

            if (!PhoneValidator.IsValidNumber(ContactPhone, Country))
                yield return new RuleViolation("Phone# does not match country", "ContactPhone");

            yield break;
        }

        public virtual ICollection<RSVP> Rsvps { get; set; }
    }
}