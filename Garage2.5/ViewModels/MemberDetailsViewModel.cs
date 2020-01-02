using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garage2._5.Models;

namespace Garage2._5.ViewModels
{
    public class MemberDetailsViewModel
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int ParkedVehiclesNo { get; set; }
        public ICollection<ParkedVehicle> ParkedVehicles { get; set; }
    }
}
