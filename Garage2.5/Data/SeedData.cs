using Garage2._5.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Bogus;
using System.Threading.Tasks;

namespace Garage2._5.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider services)
        {
           
                var option = services.GetRequiredService<DbContextOptions<Garage2_5Context>>();
            using (var context = new Garage2_5Context(option))
            {
                // Load Vehicle Type for the first time. If Data already loaded in the table do not load it again.


                var fake = new Faker("sv");
                var vehicles = new List<VehicleType>();
                var stringarr = new string[5] { "Airplane", "Boat", "Bus", "Car", "Motorbike" };
             //   var vehicleWithoutDuplicate = new List<VehicleType>();

              
          
                    if (!context.VehicleType.Any())
                     {

                    for (int i = 0; i < 5; i++)
                    {

                        //var ftype = fake.Vehicle.Type();
                        var ftype = stringarr[i];

                        
                        var vehicletype = new VehicleType

                        {
                            Type = ftype,
                        };

                        vehicles.Add(vehicletype);
                       

                    }

                    // Load only distinct vehicle Type.
                   // vehicleWithoutDuplicate = vehicles.GroupBy(x => x.Type).Select(x => x.First()).ToList();


                    context.AddRange(vehicles);
                    context.SaveChanges();
                }

            }
            }

        }
    }

