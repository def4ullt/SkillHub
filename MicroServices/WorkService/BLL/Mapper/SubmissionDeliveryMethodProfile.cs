using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO.DeliveryMethod;
using Domain.Entities;

namespace BLL.Mapper
{
    public class SubmissionDeliveryMethodProfile : Profile
    {
        public SubmissionDeliveryMethodProfile()
        {
            CreateMap<SubmissionDeliveryMethod, SubmissionDeliveryMethodReadDto>();
            CreateMap<SubmissionDeliveryMethodCreateDto, SubmissionDeliveryMethod>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(date => DateTime.UtcNow));
            CreateMap<SubmissionDeliveryMethodUpdateDto, SubmissionDeliveryMethod>();
        }
    }
}
