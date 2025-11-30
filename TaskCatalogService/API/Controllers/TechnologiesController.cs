using BLL.DTO.Technology;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing Technologies
    /// </summary>
    [ApiController]
    [Route("api/technologies")]
    public class TechnologiesController : ControllerBase
    {
        private ITechnologyService technologyService;
        private ILogger<TechnologiesController> logger;

        /// <summary>
        /// Constructor for TechnologiesController
        /// </summary>
        /// <param name="technologyService">Service for working with Technologies</param>
        /// <param name="logger">Logger instance</param>
        public TechnologiesController(ITechnologyService technologyService, ILogger<TechnologiesController> logger)
        {
            this.technologyService = technologyService;
            this.logger = logger;
        }

        /// <summary>
        /// Get all technologies
        /// </summary>
        /// <returns>List of technologies</returns>
        [HttpGet]
        public async Task<ActionResult<List<TechnologyReadDto>>> GetAll(CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to get all technologies");
            List<TechnologyReadDto> technologies = await technologyService.GetAllAsync(cancellationToken);
            
            return Ok(technologies);
        }

        /// <summary>
        /// Get a technology by Id
        /// </summary>
        /// <param name="id">Technology Id</param>
        /// <returns>Technology details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TechnologyReadDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to get technology with Id: {TechnologyId}", id);
            TechnologyReadDto technology = await technologyService.GetByIdAsync(id, cancellationToken);
            
            return Ok(technology);
        }

        /// <summary>
        /// Create a new technology
        /// </summary>
        /// <param name="dto">DTO for creating a technology</param>
        /// <returns>Created technology</returns>
        [HttpPost]
        public async Task<ActionResult<TechnologyReadDto>> Create([FromBody] TechnologyCreateDto dto, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to create new technology with name: {TechnologyName}", dto.Name);
            TechnologyReadDto createdTechnology = await technologyService.CreateAsync(dto, cancellationToken);
            
            return CreatedAtAction(nameof(GetById), new { id = createdTechnology.Id }, createdTechnology);
        }

        /// <summary>
        /// Update a technology
        /// </summary>
        /// <param name="id">Technology Id to update</param>
        /// <param name="dto">DTO for updating technology</param>
        /// <returns>Updated technology</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<TechnologyReadDto>> Update(Guid id, [FromBody] TechnologyUpdateDto dto, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to update technology with Id: {TechnologyId}", id);
            TechnologyReadDto updatedTechnology = await technologyService.UpdateAsync(id, dto, cancellationToken);
            
            return Ok(updatedTechnology);
        }

        /// <summary>
        /// Delete a technology
        /// </summary>
        /// <param name="id">Technology Id to delete</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to delete technology with Id: {TechnologyId}", id);
            await technologyService.DeleteAsync(id, cancellationToken);
            
            return NoContent();
        }
    }
}
