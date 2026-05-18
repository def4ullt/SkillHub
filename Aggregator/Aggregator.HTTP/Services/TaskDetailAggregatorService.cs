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
			var taskResponse = await http.GetAsync($"http://localhost:5015/api/tasks/{taskId}");

			if (!taskResponse.IsSuccessStatusCode)
				throw new HttpRequestException($"Task {taskId} not found", null, taskResponse.StatusCode);

			var task = await taskResponse.Content.ReadFromJsonAsync<TaskDetailDto>();

			var workTask = http.GetAsync($"http://localhost:5087/api/work-submissions/?TaskId={taskId}&Page=1&PageSize=10");
			var reviewTask = http.GetAsync($"http://localhost:5293/api/reviews/?TaskId={taskId}&Page=1&PageSize=10");
			var questionTask = http.GetAsync($"http://localhost:5293/api/questions/?TaskId={taskId}&Page=1&PageSize=10");

			await Task.WhenAll(workTask, reviewTask, questionTask);

			var workSubmissions = (await workTask).IsSuccessStatusCode
				? await (await workTask).Content.ReadFromJsonAsync<PaginatedResponse<WorkSubmissionReadDto>>()
				: new PaginatedResponse<WorkSubmissionReadDto>();

			var reviews = (await reviewTask).IsSuccessStatusCode
				? await (await reviewTask).Content.ReadFromJsonAsync<PaginatedResponse<TaskReviewDto>>()
				: new PaginatedResponse<TaskReviewDto>();

			var questions = (await questionTask).IsSuccessStatusCode
				? await (await questionTask).Content.ReadFromJsonAsync<PaginatedResponse<TaskQuestionDto>>()
				: new PaginatedResponse<TaskQuestionDto>();

			return new TaskDetailResponseDto
			{
				Task = task!,
				WorkSubmissions = workSubmissions?.Items ?? new List<WorkSubmissionReadDto>(),
				Reviews = reviews?.Items ?? new List<TaskReviewDto>(),
				Questions = questions?.Items ?? new List<TaskQuestionDto>()
			};
		}
	}
}
