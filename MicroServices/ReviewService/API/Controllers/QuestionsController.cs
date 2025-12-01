using Application.Question.Commands.AddAnswer;
using Application.Question.Commands.CreateQuestion;
using Application.Question.Commands.DeleteAnswer;
using Application.Question.Commands.DeleteQuestion;
using Application.Question.Commands.UpdateAnswer;
using Application.Question.Commands.UpdateQuestion;
using Application.Question.Query.GetQuestionById;
using Application.Question.Query.GetQuestions;
using Domain.Entities.QueryParams;
using Domain.Entities;
using Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionsController : ControllerBase
    {
        private IMediator mediator;
        private ILogger<QuestionsController> logger;

        public QuestionsController(IMediator mediator, ILogger<QuestionsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        /// <summary>
        /// Creates a new question for a specific task.
        /// </summary>
        /// <param name="command">The command containing TaskId, QuestionText and User information.</param>
        /// <returns>The Id of the newly created question.</returns>
        [HttpPost]
        public async Task<ActionResult<TaskQuestion>> CreateQuestion([FromBody] CreateTaskQuestionCommand command)
        {
            logger.LogInformation("Creating question for TaskId {TaskId}", command.TaskId);
            var id = await mediator.Send(command);

            var question = await mediator.Send(new GetQuestionByIdQuery(id));
            return CreatedAtAction(nameof(GetQuestionById), new { questionId = question.Id }, question);
        }

        /// <summary>
        /// Retrieves a question by its Id.
        /// </summary>
        /// <param name="questionId">The Id of the question to retrieve.</param>
        /// <returns>The question with answers if found.</returns>
        [HttpGet("{questionId}")]
        public async Task<ActionResult<TaskQuestion>> GetQuestionById(string questionId)
        {
            logger.LogInformation("Fetching question with Id {QuestionId}", questionId);
            var query = new GetQuestionByIdQuery(questionId);
            var question = await mediator.Send(query);
            
            return Ok(question);
        }

        /// <summary>
        /// Retrieves a paginated list of questions with optional filtering by TaskId or UserId.
        /// </summary>
        /// <param name="queryParams">Query parameters for filtering, sorting, and pagination.</param>
        [HttpGet]
        public async Task<ActionResult<PagedList<TaskQuestion>>> GetQuestions([FromQuery] TaskQuestionQueryParameters queryParams)
        {
            logger.LogInformation("Fetching questions page {PageNumber}", queryParams.PageNumber);
            var query = new GetQuestionsQuery(queryParams);
            var result = await mediator.Send(query);
            
            return Ok(result);
        }

        /// <summary>
        /// Updates the text of an existing question.
        /// </summary>
        /// <param name="command">The command containing QuestionId and new text.</param>
        [HttpPut]
        public async Task<IActionResult> UpdateQuestion([FromBody] UpdateTaskQuestionCommand command)
        {
            logger.LogInformation("Updating question {QuestionId}", command.QuestionId);
            await mediator.Send(command);
            
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing question.
        /// </summary>
        /// <param name="command">The command containing QuestionId.</param>
        [HttpDelete]
        public async Task<IActionResult> DeleteQuestion([FromBody] DeleteTaskQuestionCommand command)
        {
            logger.LogInformation("Deleting question {QuestionId}", command.QuestionId);
            await mediator.Send(command);
            
            return NoContent();
        }

        /// <summary>
        /// Adds a new answer to an existing question and returns the updated question with all answers.
        /// </summary>
        /// <param name="command">The command containing QuestionId, AnswerText, and User information.</param>
        /// <returns>The updated TaskQuestion object with all answers.</returns>
        [HttpPost("answers")]
        public async Task<ActionResult<TaskQuestion>> AddAnswer([FromBody] AddTaskAnswerCommand command)
        {
            logger.LogInformation("Adding answer to question {QuestionId}", command.QuestionId);

            await mediator.Send(command);
            var question = await mediator.Send(new GetQuestionByIdQuery(command.QuestionId));

            return CreatedAtAction(nameof(GetQuestionById), new { questionId = question.Id }, question);
        }

        /// <summary>
        /// Updates the text of an existing answer.
        /// </summary>
        /// <param name="command">The command containing QuestionId, AnswerId, and new text.</param>
        [HttpPut("answers")]
        public async Task<IActionResult> UpdateAnswer([FromBody] UpdateTaskAnswerCommand command)
        {
            logger.LogInformation("Updating answer {AnswerId} of question {QuestionId}", command.AnswerId, command.QuestionId);
            await mediator.Send(command);
            
            return NoContent();
        }

        /// <summary>
        /// Deletes an answer from a question.
        /// </summary>
        /// <param name="command">The command containing QuestionId and AnswerId.</param>
        [HttpDelete("answers")]
        public async Task<IActionResult> DeleteAnswer([FromBody] DeleteTaskAnswerCommand command)
        {
            logger.LogInformation("Deleting answer {AnswerId} from question {QuestionId}", command.AnswerId, command.QuestionId);
            await mediator.Send(command);
            
            return NoContent();
        }
    }
}
