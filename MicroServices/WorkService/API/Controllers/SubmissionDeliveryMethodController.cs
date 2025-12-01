using BLL.DTO.DeliveryMethod;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing submission delivery methods.
    /// </summary>
    [ApiController]
    [Route("api/submission-methods")]
    public class SubmissionDeliveryMethodController : ControllerBase
    {
        private ISubmissionDeliveryMethodService service;
        private ILogger<SubmissionDeliveryMethodController> logger;

        public SubmissionDeliveryMethodController(ISubmissionDeliveryMethodService service, ILogger<SubmissionDeliveryMethodController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        /// <summary>
        /// Gets all submission delivery methods.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of delivery methods.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubmissionDeliveryMethodReadDto>>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Retrieving all submission delivery methods.");

            IEnumerable<SubmissionDeliveryMethodReadDto> result = await service.GetAllAsync(cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Gets a submission delivery method by ID.
        /// </summary>
        /// <param name="id">Delivery method ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Delivery method.</returns>
        [HttpGet("{id}", Name = "GetSubmissionMethodById")]
        public async Task<ActionResult<SubmissionDeliveryMethodReadDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Retrieving submission delivery method with ID: {Id}", id);
            SubmissionDeliveryMethodReadDto dto = await service.GetByIdAsync(id, cancellationToken);
            return Ok(dto);
        }

        /// <summary>
        /// Creates a new submission delivery method.
        /// </summary>
        /// <param name="dto">Create DTO.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Created method.</returns>
        [HttpPost]
        public async Task<ActionResult<SubmissionDeliveryMethodReadDto>> CreateAsync(SubmissionDeliveryMethodCreateDto dto, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Creating new submission delivery method.");

            SubmissionDeliveryMethodReadDto created = await service.CreateAsync(dto, cancellationToken);

            return CreatedAtRoute("GetSubmissionMethodById", new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing submission delivery method.
        /// </summary>
        /// <param name="id">Delivery method ID.</param>
        /// <param name="dto">Update DTO.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Updated method.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<SubmissionDeliveryMethodReadDto>> UpdateAsync(Guid id, SubmissionDeliveryMethodUpdateDto dto, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Updating submission delivery method with ID: {Id}", id);

            SubmissionDeliveryMethodReadDto updated = await service.UpdateAsync(id, dto, cancellationToken);

            return Ok(updated);
        }

        /// <summary>
        /// Deletes a submission delivery method.
        /// </summary>
        /// <param name="id">Delivery method ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogWarning("Deleting submission delivery method with ID: {Id}", id);

            await service.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
