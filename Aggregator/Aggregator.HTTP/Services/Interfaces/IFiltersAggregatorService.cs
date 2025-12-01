using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aggregator.HTTP.DTO.FilterDTO;

namespace Aggregator.HTTP.Services.Interfaces
{
    public interface IFiltersAggregatorService
    {
        Task<FiltersResponseDto> GetFiltersAsync();
    }
}
