using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregator.HTTP.DTO.TaskDetailDTO
{
    public class TaskReviewDto
    {
        public string? Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public UserInformationDto? User { get; set; } 
    }
}
