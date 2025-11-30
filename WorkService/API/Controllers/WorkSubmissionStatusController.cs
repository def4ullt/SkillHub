using BLL.DTO.WorkSubmissionStatus;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing work submission statuses.
    /// </summary>
    [ApiController]
    [Route("api/submission-statuses")]
    public class WorkSubmissionStatusController : ControllerBase
    {
        private IWorkSubmissionStatusService service;
        private ILogger<WorkSubmissionStatusController> logger;

        public WorkSubmissionStatusController(IWorkSubmissionStatusService service, ILogger<WorkSubmissionStatusController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves all work submission statuses.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of submission statuses.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkSubmissionStatusReadDto>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Retrieving all work submission statuses.");

            IEnumerable<WorkSubmissionStatusReadDto> result = await service.GetAllAsync(cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a work submission status by ID.
        /// </summary>
        /// <param name="id">Status identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Status object.</returns>
        [HttpGet("{id}", Name = "GetSubmissionStatusById")]
        public async Task<ActionResult<WorkSubmissionStatusReadDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Retrieving work submission status with ID: {Id}", id);

            WorkSubmissionStatusReadDto dto = await service.GetByIdAsync(id, cancellationToken);

            return Ok(dto);
        }

        /// <summary>
        /// Creates a new work submission status.
        /// </summary>
        /// <param name="dto">Create DTO.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Created status.</returns>
        [HttpPost]
        public async Task<ActionResult<WorkSubmissionStatusReadDto>> CreateAsync(WorkSubmissionStatusCreateDto dto, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Creating new work submission status.");

            WorkSubmissionStatusReadDto created = await service.CreateAsync(dto, cancellationToken);

            return CreatedAtRoute("GetSubmissionStatusById", new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing work submission status.
        /// </summary>
        /// <param name="id">Status identifier.</param>
        /// <param name="dto">Update DTO.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Updated status.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<WorkSubmissionStatusReadDto>> UpdateAsync(Guid id, WorkSubmissionStatusUpdateDto dto, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Updating work submission status with ID: {Id}", id);

            WorkSubmissionStatusReadDto updated = await service.UpdateAsync(id, dto, cancellationToken);

            return Ok(updated);
        }

        /// <summary>
        /// Deletes a work submission status by ID.
        /// </summary>
        /// <param name="id">Status identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogWarning("Deleting work submission status with ID: {Id}", id);

            await service.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}