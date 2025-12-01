using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregator.HTTP.DTO.TaskDetailDTO
{
    public class TaskQuestionDto
    {
        public string? Id { get; set; }
        public string? QuestionText { get; set; } 
        public UserInformationDto? User { get; set; } 
        public List<TaskAnswerDto> Answers { get; set; } = new();
    }
}
