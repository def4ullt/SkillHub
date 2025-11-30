using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO.WorkSubmission;
using BLL.DTO.WorkSubmissionFile;
using Domain.Entities;

namespace BLL.Mapper
{
    public class WorkSubmissionProfile : Profile
    {
        public WorkSubmissionProfile()
        {
            CreateMap<WorkSubmission, WorkSubmissionReadDto>();
            CreateMap<WorkSubmissionDetail, WorkSubmissionDetailDto>()
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files));

            CreateMap<WorkSubmissionCreateDto, WorkSubmission>()
                .ForMember(dest => dest.SubmissionDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<WorkSubmissionUpdateDto, WorkSubmission>();

            CreateMap<WorkSubmissionFile, WorkSubmissionFileReadDto>();

            CreateMap<WorkSubmissionFileCreateDto, WorkSubmissionFile>();
            CreateMap<WorkSubmissionFileUpdateDto, WorkSubmissionFile>();
        }
    }
}
