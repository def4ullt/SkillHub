using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.QueryParams;
using Domain.Entities;
using Domain.Helpers;

namespace Domain.Interfaces
{
    public interface ITaskQuestionRepository : IGenericRepository<TaskQuestion>
    {
        Task UpdateQuestionAsync(TaskQuestion question, CancellationToken cancellationToken = default);
        Task<PagedList<TaskQuestion>> GetQuestionsAsync(TaskQuestionQueryParameters queryParameters, CancellationToken cancellationToken = default);
        Task AddAnswerAsync(string questionId, TaskAnswer answer, CancellationToken cancellationToken = default);
        Task UpdateAnswerAsync(string questionId, TaskAnswer answer, CancellationToken cancellationToken = default);
        Task DeleteAnswerAsync(string questionId, string answerId, CancellationToken cancellationToken = default);
    }
}
