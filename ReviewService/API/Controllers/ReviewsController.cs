using Application.Reviews.Commands.CreateReview;
using Application.Reviews.Commands.DeleteReview;
using Application.Reviews.Commands.UpdateReview;
using Application.Reviews.Query.GetReviewById;
using Application.Reviews.Query.GetReviews;
using Domain.Entities.QueryParams;
using Domain.Entities;
using Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private IMediator mediator;
        private ILogger<ReviewsController> logger;

        public ReviewsController(IMediator mediator, ILogger<ReviewsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        /// <summary>
        /// Creates a new review for a specific task.
        /// </summary>
        /// <param name="command">The command containing TaskId, Rating, Comment and User information.</param>
        /// <returns>The Id of the newly created review.</returns>
        [HttpPost("create")]
        public async Task<ActionResult<TaskReview>> CreateReview([FromBody] CreateTaskReviewCommand command)
        {
            logger.LogInformation("Creating review for TaskId {TaskId} by User {UserId}", command.TaskId, command.User.UserId);
            var id = await mediator.Send(command);

            var review = await mediator.Send(new GetReviewByIdQuery(id));
            return CreatedAtAction(nameof(GetReviewById), new { reviewId = review.Id }, review);
        }

        /// <summary>
        /// Retrieves a review by its Id.
        /// </summary>
        /// <param name="reviewId">The Id of the review to retrieve.</param>
        /// <returns>The review if found.</returns>
        [HttpGet("{reviewId}")]
        public async Task<ActionResult<TaskReview>> GetReviewById(string reviewId)
        {
            logger.LogInformation("Fetching review with Id {ReviewId}", reviewId);
            var query = new GetReviewByIdQuery(reviewId);
            var review = await mediator.Send(query);
            
            return Ok(review);
        }

        /// <summary>
        /// Retrieves a paginated list of reviews with optional filtering by TaskId or UserId.
        /// </summary>
        /// <param name="queryParams">Query parameters for filtering, sorting, and pagination.</param>
        [HttpGet("list")]
        public async Task<ActionResult<PagedList<TaskReview>>> GetReviews([FromQuery] TaskReviewQueryParameters queryParams)
        {
            logger.LogInformation("Fetching reviews page {PageNumber}", queryParams.PageNumber);
            var query = new GetReviewsQuery(queryParams);
            var result = await mediator.Send(query);
            
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing review.
        /// </summary>
        /// <param name="command">The command containing ReviewId, new Rating, and Comment.</param>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateTaskReviewCommand command)
        {
            logger.LogInformation("Updating review {ReviewId}", command.ReviewId);
            await mediator.Send(command);
            
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing review.
        /// </summary>
        /// <param name="command">The command containing ReviewId.</param>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteReview([FromBody] DeleteTaskReviewCommand command)
        {
            logger.LogInformation("Deleting review {ReviewId}", command.ReviewId);
            await mediator.Send(command);
            
            return NoContent();
        }
    }
}
