using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Garage2._5.Models
{
    public class Garage2_5Context : DbContext
    {
        public Garage2_5Context (DbContextOptions<Garage2_5Context> options)
            : base(options)
        {
        }

        public DbSet<ParkedVehicle> ParkedVehicle { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
    }
}
