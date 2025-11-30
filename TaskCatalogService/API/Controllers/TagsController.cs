using BLL.DTO.Tag;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing Tags
    /// </summary>
    [ApiController]
    [Route("api/tags")]
    public class TagsController : ControllerBase
    {
        private ITagService tagService;
        private ILogger<TagsController> logger;

        /// <summary>
        /// Constructor for TagsController
        /// </summary>
        /// <param name="tagService">Service for working with Tags</param>
        /// <param name="logger">Logger instance</param>
        public TagsController(ITagService tagService, ILogger<TagsController> logger)
        {
            this.tagService = tagService;
            this.logger = logger;
        }

        /// <summary>
        /// Get all tags
        /// </summary>
        /// <returns>List of tags</returns>
        [HttpGet]
        public async Task<ActionResult<List<TagReadDto>>> GetAll(CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to get all tags");
            List<TagReadDto> tags = await tagService.GetAllAsync(cancellationToken);
            return Ok(tags);
        }

        /// <summary>
        /// Get a tag by Id
        /// </summary>
        /// <param name="id">Tag Id</param>
        /// <returns>Tag details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TagReadDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to get tag with Id: {TagId}", id);
            TagReadDto tag = await tagService.GetByIdAsync(id, cancellationToken);
            return Ok(tag);
        }

        /// <summary>
        /// Create a new tag
        /// </summary>
        /// <param name="dto">DTO for creating a tag</param>
        /// <returns>Created tag</returns>
        [HttpPost]
        public async Task<ActionResult<TagReadDto>> Create([FromBody] TagCreateDto dto, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to create new tag with name: {TagName}", dto.Name);
            TagReadDto createdTag = await tagService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdTag.Id }, createdTag);
        }

        /// <summary>
        /// Update a tag
        /// </summary>
        /// <param name="id">Tag Id to update</param>
        /// <param name="dto">DTO for updating tag</param>
        /// <returns>Updated tag</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<TagReadDto>> Update(Guid id, [FromBody] TagUpdateDto dto, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to update tag with Id: {TagId}", id);
            TagReadDto updatedTag = await tagService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updatedTag);
        }

        /// <summary>
        /// Delete a tag
        /// </summary>
        /// <param name="id">Tag Id to delete</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to delete tag with Id: {TagId}", id);
            await tagService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
