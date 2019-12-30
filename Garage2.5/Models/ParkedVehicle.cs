using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._5.Models
{
    public class ParkedVehicle
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int NoOfWheels { get; set; }       
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public int MemberId { get; set; }
        public int VehicleTypeId { get; set; }

        //Navigationproperty Not in database!
        public Member Member { get; set; }
        public VehicleType VehicleType { get; set; }
    }
}
