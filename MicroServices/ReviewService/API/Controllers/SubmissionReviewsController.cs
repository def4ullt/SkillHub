using Application.SubmissionReviews.Commands.CreateSubmissionReview;
using Application.SubmissionReviews.Commands.DeleteSubmissionReview;
using Application.SubmissionReviews.Queries.GetSubmissionReviews;
using Domain.Entities;
using Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/submission-reviews")]
    public class SubmissionReviewsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<SubmissionReviewsController> logger;

        public SubmissionReviewsController(IMediator mediator, ILogger<SubmissionReviewsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        /// <summary>
        /// Creates a mentor feedback for a specific submission.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SubmissionReview>> Create([FromBody] CreateSubmissionReviewCommand command)
        {
            logger.LogInformation("Creating submission review for SubmissionId {SubmissionId}", command.SubmissionId);
            var id = await mediator.Send(command);

            var reviews = await mediator.Send(new GetSubmissionReviewsQuery(command.SubmissionId, null, 1, 50));
            var review = reviews.Items.FirstOrDefault(r => r.Id == id);
            return CreatedAtAction(nameof(GetAll), new { submissionId = command.SubmissionId }, review);
        }

        /// <summary>
        /// Retrieves submission reviews filtered by submissionId or taskId.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedList<SubmissionReview>>> GetAll(
            [FromQuery] Guid? submissionId,
            [FromQuery] Guid? taskId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            logger.LogInformation("Fetching submission reviews");
            var result = await mediator.Send(new GetSubmissionReviewsQuery(submissionId, taskId, pageNumber, pageSize));
            return Ok(result);
        }

        /// <summary>
        /// Deletes a submission review.
        /// </summary>
        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> Delete(string reviewId)
        {
            logger.LogInformation("Deleting submission review {ReviewId}", reviewId);
            await mediator.Send(new DeleteSubmissionReviewCommand(reviewId));
            return NoContent();
        }
    }
}
