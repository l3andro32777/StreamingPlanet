using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace StreamingPlanet.Models
{
    public class CinemaUser : IdentityUser
    {
        //[PersonalData]
        public String? FullName { get; set; }

        public DateTime BirthDate { get; set; }

        //Payment Information
        //[ProtectedPersonalData]
        public String? CardNumber { get; set; }

        //[ProtectedPersonalData]
        public String? ExpirationDate { get; set; }

        //[ProtectedPersonalData]
        [Range(111, 999)]

        public int CCV { get; set; }

        //Billing Address
        //[ProtectedPersonalData]
        public String? Address1 { get; set; }
        //[ProtectedPersonalData]
        public String? Address2 { get; set; }

        //[ProtectedPersonalData]
        public String? PostalCode { get; set; }

        //[ProtectedPersonalData]
        public String? City { get; set; }
        //[ProtectedPersonalData]
        public String? Country { get; set; }

    }
    
}
