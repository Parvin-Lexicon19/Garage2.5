using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Garage2._5.ViewModels
{
    public class VehicleListDetails
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        public string Type { get; set; }

        [Display(Name = "CheckIn Time")]
        public DateTime CheckInTime { get; set; }

        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }



    }
}
