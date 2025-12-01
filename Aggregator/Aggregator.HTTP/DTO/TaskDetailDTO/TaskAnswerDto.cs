using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregator.HTTP.DTO.TaskDetailDTO
{
    public class TaskAnswerDto
    {
        public string? AnswerText { get; set; } 
        public UserInformationDto? User { get; set; } 
    }
}
