using AutoMapper;
using parky_project.API.Models;
using Parky_project.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parky_project.BL.Mapping
{
    public class MapperClass:Profile
    {
        public MapperClass()
        {
            CreateMap<NationalPark,NationalParkDto>()
                
                .ForPath(des=>des.Name,src=>src.MapFrom(src=>src.Name))
                .ForPath(des=>des.Created,src=>src.MapFrom(src=>src.Created))
                .ForPath(des=>des.Established,src=>src.MapFrom(src=>src.Established))
                .ReverseMap();
            
        }
    }
}
