using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Tag;
using BLL.DTO.Technology;

namespace Aggregator.HTTP.DTO.FilterDTO
{
    public class FiltersResponseDto
    {
        public List<TagReadDto> Tags { get; set; } = new();
        public List<TechnologyReadDto> Technologies { get; set; } = new();
    }
}
