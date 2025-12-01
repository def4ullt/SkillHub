using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.QueryParams;
using Domain.Helpers;
using Domain.Interfaces;
using Infrastructure.Context;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class TaskQuestionRepository : GenericRepository<TaskQuestion>, ITaskQuestionRepository
    {
        public TaskQuestionRepository(MongoDbContext context, IClientSessionHandle? session = null) : base(context, session)
        {

        }

        public async Task<PagedList<TaskQuestion>> GetQuestionsAsync(TaskQuestionQueryParameters queryParameters, CancellationToken cancellationToken = default)
        {
            var filterBuilder = Builders<TaskQuestion>.Filter;
            var filter = filterBuilder.Empty;

            if (queryParameters.UserId.HasValue)
            {
                filter &= filterBuilder.Eq(question => question.User.UserId, queryParameters.UserId.Value);
            }

            if (queryParameters.TaskId.HasValue)
            {
                filter &= filterBuilder.Eq(question => question.TaskId, queryParameters.TaskId.Value);
            }

            IFindFluent<TaskQuestion, TaskQuestion> findFluent = collection.Find(filter);

            if (!string.IsNullOrWhiteSpace(queryParameters.OrderBy))
            {
                MongoSortHelper<TaskQuestion> sortHelper = new MongoSortHelper<TaskQuestion>();
                findFluent = findFluent.Sort(sortHelper.ApplySort(queryParameters.OrderBy));
            }

            return await PagedList<TaskQuestion>.ToPagedListAsync(findFluent, queryParameters.PageNumber, queryParameters.PageSize, cancellationToken);
        }

        public async Task AddAnswerAsync(string questionId, TaskAnswer answer, CancellationToken cancellationToken = default)
        {
            var update = Builders<TaskQuestion>.Update.Push(question => question.Answers, answer);

            await collection.UpdateOneAsync(question => question.Id == questionId, update, cancellationToken: cancellationToken);
        }

        public async Task UpdateAnswerAsync(string questionId, TaskAnswer answer, CancellationToken cancellationToken = default)
        {
            var filter = Builders<TaskQuestion>.Filter.And(
                Builders<TaskQuestion>.Filter.Eq(question => question.Id, questionId),
                Builders<TaskQuestion>.Filter.ElemMatch(question => question.Answers, a => a.Id == answer.Id)
            );

            var update = Builders<TaskQuestion>.Update
                .Set(q => q.Answers[-1], answer);

            await collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        }

        public async Task UpdateQuestionAsync(TaskQuestion question, CancellationToken cancellationToken = default)
        {
            var update = Builders<TaskQuestion>.Update
                .Set(tempQuestion => tempQuestion.QuestionText, question.QuestionText);

            await collection.UpdateOneAsync(q => q.Id == question.Id, update, cancellationToken: cancellationToken);
        }

        public async Task DeleteAnswerAsync(string questionId, string answerId, CancellationToken cancellationToken = default)
        {
            var update = Builders<TaskQuestion>.Update.PullFilter(question => question.Answers, answer => answer.Id == answerId);

            await collection.UpdateOneAsync(question => question.Id == questionId, update, cancellationToken: cancellationToken);
        }
    }
}
