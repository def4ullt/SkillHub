using BLL.DTO.Task;
using BLL.Services.Interfaces;
using DAL.Pagination;
using Domain.Query;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing Tasks
    /// </summary>
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private ITaskService taskService;
        private ILogger<TasksController> logger;

        /// <summary>
        /// Constructor for TasksController
        /// </summary>
        /// <param name="taskService">Service for working with Tasks</param>
        /// <param name="logger">Logger instance</param>
        public TasksController(ITaskService taskService, ILogger<TasksController> logger)
        {
            this.taskService = taskService;
            this.logger = logger;
        }

        /// <summary>
        /// Get paged list of tasks
        /// </summary>
        /// <param name="parameters">Query parameters for filtering, sorting, and paging</param>
        /// <returns>Paged list of tasks</returns>
        [HttpGet]
        public async Task<ActionResult<PagedList<TaskReadDto>>> GetPaged([FromQuery] TaskQueryParameters parameters, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to get paged tasks");
            var tasks = await taskService.GetPagedTasksAsync(parameters, cancellationToken);
            
            return Ok(tasks);
        }

        /// <summary>
        /// Get a task by Id with detailed information
        /// </summary>
        /// <param name="id">Task Id</param>
        /// <returns>Task details including Technologies and Tags</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDetailDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to get task with Id: {TaskId}", id);
            TaskDetailDto? task = await taskService.GetTaskByIdAsync(id, cancellationToken);
            
            return Ok(task);
        }

        /// <summary>
        /// Create a new task
        /// </summary>
        /// <param name="dto">DTO for creating a task</param>
        /// <returns>Created task details</returns>
        [HttpPost]
        public async Task<ActionResult<TaskDetailDto>> Create([FromBody] TaskCreateDto dto, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to create new task with title: {TaskTitle}", dto.Title);
            TaskDetailDto createdTask = await taskService.CreateTaskAsync(dto, cancellationToken);
            
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }

        /// <summary>
        /// Update an existing task
        /// </summary>
        /// <param name="id">Task Id to update</param>
        /// <param name="dto">DTO for updating the task</param>
        /// <returns>Updated task details</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDetailDto>> Update(Guid id, [FromBody] TaskUpdateDto dto, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to update task with Id: {TaskId}", id);
            TaskDetailDto updatedTask = await taskService.UpdateTaskAsync(id, dto, cancellationToken);
            
            return Ok(updatedTask);
        }

        /// <summary>
        /// Delete a task
        /// </summary>
        /// <param name="id">Task Id to delete</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to delete task with Id: {TaskId}", id);
            await taskService.DeleteTaskAsync(id, cancellationToken);
            
            return NoContent();
        }
    }
}
