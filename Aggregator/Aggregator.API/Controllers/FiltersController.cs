using Aggregator.HTTP.DTO.FilterDTO;
using Aggregator.HTTP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.API.Controllers
{
    /// <summary>
    /// Controller responsible for providing aggregated filters data.
    /// Combines all available tags and technologies into a single response
    /// for easier consumption by frontend applications.
    /// </summary>
    [ApiController]
    [Route("api/filters")]
    public class FiltersController : ControllerBase
    {
        private IFiltersAggregatorService service;

        /// <summary>
        /// Constructor for FiltersController.
        /// </summary>
        /// <param name="service">Service responsible for aggregating filters data.</param>
        public FiltersController(IFiltersAggregatorService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Retrieves all tags and technologies in a combined format.
        /// This endpoint fetches data from multiple services (Tags and Technologies)
        /// and returns a single consolidated response.
        /// </summary>
        /// <returns>
        /// Returns <see cref="FiltersResponseDto"/> containing lists of tags and technologies.
        /// </returns>
        /// <response code="200">Returns the combined filters successfully.</response>
        /// <response code="500">If an internal server error occurs while fetching data from services.</response>
        [HttpGet]
        [ProducesResponseType(typeof(FiltersResponseDto), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<FiltersResponseDto>> GetFilters()
        {
            var result = await service.GetFiltersAsync();
            return Ok(result);
        }
    }
}