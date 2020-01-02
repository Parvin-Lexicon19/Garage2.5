using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Garage2._5.Models;
using Garage2._5.ViewModel;

namespace Garage2._5.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ParkedVehicle, VehicleListDetails>()
               .ForMember(
                       dest => dest.Type,
                       from => from.MapFrom(s => s.VehicleType.Type))
               .ForMember(
                       dest => dest.OwnerName,
                       from => from.MapFrom(s => s.Member.FullName));


              /* .ForMember(
                      dest => dest.FirstName,
                      from => from.MapFrom(s => s.Member.FirstName))
               .ForMember(
                      dest => dest.LastName,
                      from => from.MapFrom(s => s.Member.LastName)); */

        }
    }
}
