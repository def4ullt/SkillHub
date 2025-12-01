using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aggregator.HTTP.DTO.TaskDetailDTO;

namespace Aggregator.HTTP.Services.Interfaces
{
    public interface ITaskDetailAggregatorService
    {
        Task<TaskDetailResponseDto> GetTaskDetailAsync(Guid taskId);
    }
}
