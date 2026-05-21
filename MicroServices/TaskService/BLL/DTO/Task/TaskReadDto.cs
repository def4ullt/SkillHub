using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace BLL.DTO.Task
{
    public class TaskReadDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } 
        public string? Description { get; set; } 
        public Difficulty Difficulty { get; set; }
        public int EstimatedTimeMinutes { get; set; }
        public int XpReward { get; set; }
        public bool IsActive { get; set; }
        public Guid AuthorId { get; set; }
    }
}
