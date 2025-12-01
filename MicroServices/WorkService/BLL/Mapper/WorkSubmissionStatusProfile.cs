using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO.WorkSubmissionStatus;
using Domain.Entities;

namespace BLL.Mapper
{
    public class WorkSubmissionStatusProfile : Profile
    {
        public WorkSubmissionStatusProfile()
        {
            CreateMap<WorkSubmissionStatus, WorkSubmissionStatusReadDto>();
            CreateMap<WorkSubmissionStatusCreateDto, WorkSubmissionStatus>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<WorkSubmissionStatusUpdateDto, WorkSubmissionStatus>();
        }
    }
}
