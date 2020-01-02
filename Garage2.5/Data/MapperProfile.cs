using AutoMapper;
using Garage2._5.Models;
using Garage2._5.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._5.Data
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Member, MemberDetailsViewModel>()
                .ForMember(
                        dest => dest.ParkedVehiclesNo,
                        from => from.MapFrom(m => m.ParkedVehicles.Count));

                //.ForMember(
                //       dest => dest.ParkedVehicles,
                //       from => from.MapFrom(m => m.ParkedVehicles.ToList()));
        }
    }
}
