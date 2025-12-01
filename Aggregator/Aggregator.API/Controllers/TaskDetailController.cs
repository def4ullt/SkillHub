using Aggregator.HTTP.DTO.TaskDetailDTO;
using Aggregator.HTTP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.API.Controllers
{
    /// <summary>
    /// Controller responsible for providing detailed information about tasks.
    /// Aggregates data from multiple services including Task, WorkSubmission, and Review services.
    /// Returns a consolidated response for easier consumption by frontend applications.
    /// </summary>
    [ApiController]
    [Route("api/task")]
    public class TaskDetailController : ControllerBase
    {
        private ITaskDetailAggregatorService service;

        /// <summary>
        /// Constructor for TaskDetailController.
        /// </summary>
        /// <param name="service">Service responsible for aggregating task detail data.</param>
        public TaskDetailController(ITaskDetailAggregatorService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Retrieves detailed information about a specific task, including:
        /// - Task properties
        /// - First 10 work submissions
        /// - First 10 reviews
        /// - First 10 questions with their answers
        /// </summary>
        /// <param name="taskId">The ID of the task to retrieve</param>
        /// <returns>
        /// Returns <see cref="TaskDetailResponseDto"/> containing all aggregated data.
        /// </returns>
        /// <response code="200">Returns the aggregated task details successfully.</response>
        /// <response code="404">If the task with the specified ID is not found.</response>
        /// <response code="500">If an internal server error occurs while fetching data from services.</response>
        [HttpGet("{taskId:guid}")]
        [ProducesResponseType(typeof(TaskDetailResponseDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TaskDetailResponseDto>> GetTaskDetail(Guid taskId)
        {
            var result = await service.GetTaskDetailAsync(taskId);

            return Ok(result);
        }
    }
}