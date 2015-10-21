using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NerdDinner.Models
{
    public class RSVP
    {
        public int RsvpId { get; set; }
        public int DinnerId { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        public string AttendeeEmail { get; set; }

        public virtual Dinner Dinner { get; set; }
    }
}