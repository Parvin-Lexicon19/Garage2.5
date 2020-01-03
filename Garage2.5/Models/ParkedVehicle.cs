using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._5.Models
{
    public class ParkedVehicle
    {
        public int Id { get; set; }
        [MaxLength(20)]

        [Remote(action: "CheckRegno", controller: "ParkedVehicles", AdditionalFields="Id")]
        [Required]
        public string RegNo { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        [Range(0, 20)]
        public int NoOfWheels { get; set; }
        [DisplayFormat(DataFormatString = "{0:f}")]
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }

     
        [Required(ErrorMessage = "Member is Required")]
        
        public int MemberId { get; set; }
        [Required(ErrorMessage = "Vehicle Type is Required")]
        public int VehicleTypeId { get; set; }

        //Navigationproperty Not in database!
        public Member Member { get; set; }
        public VehicleType VehicleType { get; set; }
    }
}
