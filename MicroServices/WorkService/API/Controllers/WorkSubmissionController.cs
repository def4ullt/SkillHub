using BLL.DTO.WorkSubmission;
using BLL.Services.Interfaces;
using DAL.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller responsible for managing work submissions and their files.
    /// </summary>
    [ApiController]
    [Route("api/work-submissions")]
    public class WorkSubmissionController : ControllerBase
    {
        private IWorkSubmissionService service;
        private ILogger<WorkSubmissionController> logger;

        public WorkSubmissionController(IWorkSubmissionService service, ILogger<WorkSubmissionController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves a paginated list of work submissions.
        /// </summary>
        /// <param name="queryParams">Filtering and pagination parameters.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Paged list of work submissions.</returns>
        [HttpGet]
        public async Task<ActionResult<PagedList<WorkSubmissionReadDto>>> GetPagedAsync([FromQuery] WorkSubmissionQueryParams queryParams, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Retrieving paginated work submissions.");

            PagedList<WorkSubmissionReadDto> result = await service.GetPagedAsync(queryParams, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Retrieves detailed information about a specific work submission.
        /// </summary>
        /// <param name="id">Submission identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Detailed submission DTO.</returns>
        [HttpGet("{id}/detail", Name = "GetWorkSubmissionDetail")]
        public async Task<ActionResult<WorkSubmissionDetailDto>> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Retrieving detailed work submission with ID: {Id}", id);

            WorkSubmissionDetailDto? dto = await service.GetDetailAsync(id, cancellationToken);

            return Ok(dto);
        }

        /// <summary>
        /// Creates a new work submission with files.
        /// </summary>
        /// <param name="dto">Create DTO.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Created submission.</returns>
        [HttpPost]
        public async Task<ActionResult<WorkSubmissionReadDto>> CreateAsync(WorkSubmissionCreateDto dto, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Creating a new work submission.");

            WorkSubmissionReadDto created = await service.CreateAsync(dto, cancellationToken);

            return CreatedAtRoute("GetWorkSubmissionDetail", new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing work submission, replacing all files.
        /// </summary>
        /// <param name="id">Submission identifier.</param>
        /// <param name="dto">Update DTO.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Updated submission.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<WorkSubmissionReadDto>> UpdateAsync(Guid id, WorkSubmissionUpdateDto dto, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Updating work submission with ID: {Id}", id);

            WorkSubmissionReadDto updated = await service.UpdateAsync(id, dto, cancellationToken);

            return Ok(updated);
        }

        /// <summary>
        /// Deletes a work submission by its ID.
        /// </summary>
        /// <param name="id">Submission identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogWarning("Deleting work submission with ID: {Id}", id);

            await service.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}