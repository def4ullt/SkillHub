using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO.Task;
using Domain.Entities;

namespace BLL.Mapper
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskEntity, TaskReadDto>();

            CreateMap<TaskEntity, TaskDetailDto>()
                .ForMember(dest => dest.Technologies,
                           opt => opt.MapFrom(src => src.TaskTechnologies.Select(tt => tt.Technology)))
                .ForMember(dest => dest.Tags,
                           opt => opt.MapFrom(src => src.TaskTags.Select(tt => tt.Tag)));

            CreateMap<TaskCreateDto, TaskEntity>();
            CreateMap<TaskUpdateDto, TaskEntity>()
                .ForMember(dest => dest.AuthorId, opt => opt.Ignore());
        }
    }
}
