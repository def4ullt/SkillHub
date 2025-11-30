using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; } 
        public Difficulty Difficulty { get; set; }
        public int EstimatedTimeMinutes { get; set; }
        public int XpReward { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        public ICollection<TaskTechnology> TaskTechnologies { get; set; } = new List<TaskTechnology>();
        public ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
    }
}
