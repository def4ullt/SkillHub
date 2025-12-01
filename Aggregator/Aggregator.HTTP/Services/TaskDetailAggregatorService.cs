using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Aggregator.HTTP.DTO;
using Aggregator.HTTP.DTO.TaskDetailDTO;
using Aggregator.HTTP.Services.Interfaces;
using BLL.DTO.Task;
using BLL.DTO.WorkSubmission;
using DAL.Pagination;

namespace Aggregator.HTTP.Services
{
    public class TaskDetailAggregatorService : ITaskDetailAggregatorService
    {
        private HttpClient http;

        public TaskDetailAggregatorService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<TaskDetailResponseDto> GetTaskDetailAsync(Guid taskId)
        {
            var taskTask = http.GetFromJsonAsync<TaskDetailDto>($"http://localhost:5000/tasks/tasks/{taskId}");
            var workTask = http.GetFromJsonAsync<PaginatedResponse<WorkSubmissionReadDto>>($"http://localhost:5000/work/work-submissions/?TaskId={taskId}&Page=1&PageSize=10");
            var reviewTask = http.GetFromJsonAsync<PaginatedResponse<TaskReviewDto>>($"http://localhost:5000/reviews/reviews/?TaskId={taskId}&Page=1&PageSize=10");
            var questionTask = http.GetFromJsonAsync<PaginatedResponse<TaskQuestionDto>>($"http://localhost:5000/reviews/questions/?TaskId={taskId}&Page=1&PageSize=10");

            await Task.WhenAll(taskTask, workTask, reviewTask, questionTask);

            return new TaskDetailResponseDto
            {
                Task = taskTask.Result!,
                WorkSubmissions = workTask.Result?.Items ?? new List<WorkSubmissionReadDto>(),
                Reviews = reviewTask.Result?.Items ?? new List<TaskReviewDto>(),
                Questions = questionTask.Result?.Items ?? new List<TaskQuestionDto>()
            };
        }
    }
}
