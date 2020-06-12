using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanteraMera_v2.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string StreetAddress { get; set; }
        [PersonalData]
        public string PostalCode { get; set; }
        [PersonalData]
        public string City { get; set; }

    }
}
