using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Task;
using BLL.DTO.WorkSubmission;

namespace Aggregator.HTTP.DTO.TaskDetailDTO
{
    public class TaskDetailResponseDto
    {
        public TaskDetailDto? Task { get; set; }
        public List<WorkSubmissionReadDto> WorkSubmissions { get; set; } = new();
        public List<TaskReviewDto> Reviews { get; set; } = new();
        public List<TaskQuestionDto> Questions { get; set; } = new();
    }
}
