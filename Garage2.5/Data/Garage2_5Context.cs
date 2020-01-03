using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage2._5.Models;
using Garage2._5.ViewModels;

namespace Garage2._5.Data
{
    public class Garage2_5Context : DbContext
    {
        public Garage2_5Context (DbContextOptions<Garage2_5Context> options)
            : base(options)
        {
        }

        public DbSet<Garage2._5.Models.ParkedVehicle> ParkedVehicle { get; set; }

        public DbSet<Garage2._5.Models.Member> Member { get; set; }

        public DbSet<Garage2._5.Models.VehicleType> VehicleType { get; set; }

        public DbSet<Garage2._5.ViewModels.VehicleListDetails> VehicleListDetails { get; set; }
    }
}
