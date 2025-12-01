using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Query
{
    public class TaskQueryParameters
    {
        public string? Title { get; set; }
        public Difficulty? Difficulty { get; set; }
        public int? EstimatedTimeMin { get; set; }
        public int? EstimatedTimeMax { get; set; }
        public int? XpRewardMin { get; set; }
        public int? XpRewardMax { get; set; }
        public bool? IsActive { get; set; }

        public List<Guid>? TechnologyIds { get; set; }
        public List<Guid>? TagIds { get; set; }

        public string? SortBy { get; set; } 
        public bool SortDesc { get; set; } = false;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
