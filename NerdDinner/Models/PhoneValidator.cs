using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NerdDinner.Models
{
    public class PhoneValidator : ValidationAttribute
    {
        public string OtherProperty { get; set; }

        static IDictionary<string, Regex> countryRegex = new Dictionary<string, Regex>() { 
            { "USA", new Regex("^[2-9]\\d{2}-\\d{3}-\\d{4}$")},
            { "UK", new Regex("(^1300\\d{6}$)|(^1800|1900|1902\\d{6}$)|(^0[2|3|7|8]{1}[0-9]{8}$)|(^13\\d{4}$)|(^04\\d{2,3}\\d{6}$)")},
            { "Netherlands", new Regex("(^\\+[0-9]{2}|^\\+[0-9]{2}\\(0\\)|^\\(\\+[0-9]{2}\\)\\(0\\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\\-\\s]{10}$)")},
        };

        public override bool IsValid(object value)
        {
            bool isValid = false;
            Dinner dinner = value as Dinner;
            if (dinner != null)
            {
                string country = dinner.Country;
                string phoneNumber = dinner.ContactPhone;
                if (country != null && countryRegex.ContainsKey(country))
                {
                    isValid = countryRegex[country].IsMatch(phoneNumber);
                }
            }
            return isValid;
        }

        public static IEnumerable<string> AllCountries()
        {
            return countryRegex.Keys;
        } 
    }
}