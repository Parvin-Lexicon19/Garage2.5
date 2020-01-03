using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._5.ViewModels
{
    public class Statistics
    {
        [Display(Name = "Total Cars:")]
        public int TotalCar { get; set; }

        [Display(Name = "Total Airplanes:")]
        public int TotalAirplane { get; set; }

        [Display(Name = "Total Buses:")]
        public int TotalBus { get; set; }

        [Display(Name = "Total Boats:")]
        public int TotalBoat { get; set; }

        [Display(Name = "Total Motorbikes:")]
        public int TotalMotorbike { get; set; }

        [Display(Name = "Total Vehicles:")]
        public int TotalVehicles { get; set; }

        [Display(Name = "Total Wheels:")]
        public int TotalWheels { get; set; }

        [Display(Name = "Total Parked Vehicles Price:")]
        public string TotalParkedVehiclePrice { get; set; }
    }
}
