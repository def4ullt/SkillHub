using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Tag;
using BLL.DTO.Technology;
using Domain.Entities;

namespace BLL.DTO.Task
{
    public class TaskDetailDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } 
        public string? Description { get; set; } 
        public Difficulty Difficulty { get; set; }
        public int EstimatedTimeMinutes { get; set; }
        public int XpReward { get; set; }
        public bool IsActive { get; set; }
        public Guid AuthorId { get; set; }
        public List<TechnologyReadDto> Technologies { get; set; } = new List<TechnologyReadDto>();
        public List<TagReadDto> Tags { get; set; } = new List<TagReadDto>();
    }
}
