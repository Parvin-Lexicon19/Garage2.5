using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._5.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Remote(action: "CheckEmail", controller: "Members")]
        public string Email { get; set; }

        [Display(Name = "Registered Name")]
        public string FullName => $"{FirstName} {LastName}";

        public ICollection<ParkedVehicle> ParkedVehicles { get; set; }
    }
}
