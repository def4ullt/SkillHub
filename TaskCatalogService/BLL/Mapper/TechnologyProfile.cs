using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO.Technology;
using Domain.Entities;

namespace BLL.Mapper
{
    public class TechnologyProfile : Profile
    {
        public TechnologyProfile()
        {
            CreateMap<Technology, TechnologyReadDto>();
            CreateMap<TechnologyCreateDto, Technology>();
            CreateMap<TechnologyUpdateDto, Technology>();
        }
    }
}
