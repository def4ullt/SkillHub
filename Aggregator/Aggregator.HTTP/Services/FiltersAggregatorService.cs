using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Aggregator.HTTP.DTO.FilterDTO;
using Aggregator.HTTP.Services.Interfaces;
using BLL.DTO.Tag;
using BLL.DTO.Technology;

namespace Aggregator.HTTP.Services
{
    public class FiltersAggregatorService : IFiltersAggregatorService
    {
        private HttpClient http;

        public FiltersAggregatorService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<FiltersResponseDto> GetFiltersAsync()
        {
            var tagsTask = http.GetFromJsonAsync<List<TagReadDto>>("http://localhost:5000/tasks/tags/");
            var techTask = http.GetFromJsonAsync<List<TechnologyReadDto>>("http://localhost:5000/tasks/technologies/");

            await Task.WhenAll(tagsTask, techTask);

            return new FiltersResponseDto
            {
                Tags = tagsTask.Result,
                Technologies = techTask.Result
            };
        }
    }
}
