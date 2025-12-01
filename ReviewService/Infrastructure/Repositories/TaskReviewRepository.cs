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
    public class TaskReviewRepository : GenericRepository<TaskReview>, ITaskReviewRepository
    {
        public TaskReviewRepository(MongoDbContext context, IClientSessionHandle? session = null) : base(context, session)
        {

        }
        public async Task<PagedList<TaskReview>> GetReviewsAsync(TaskReviewQueryParameters queryParameters, CancellationToken cancellationToken = default)
        {
            var filterBuilder = Builders<TaskReview>.Filter;
            var filter = filterBuilder.Empty;

            if (queryParameters.UserId.HasValue) filter &= filterBuilder.Eq(review => review.User.UserId, queryParameters.UserId.Value);

            if (queryParameters.TaskId.HasValue)
            {
                filter &= filterBuilder.Eq(review => review.TaskId, queryParameters.TaskId.Value);
            }

            if (queryParameters.Rating.HasValue)
            {
                filter &= filterBuilder.Eq(review => review.Rating, queryParameters.Rating.Value);
            }

            IFindFluent<TaskReview, TaskReview> findFluent = collection.Find(filter);

            if (!string.IsNullOrWhiteSpace(queryParameters.OrderBy))
            {
                MongoSortHelper<TaskReview> sortHelper = new MongoSortHelper<TaskReview>();
                findFluent = findFluent.Sort(sortHelper.ApplySort(queryParameters.OrderBy));
            }

            return await PagedList<TaskReview>.ToPagedListAsync(findFluent, queryParameters.PageNumber, queryParameters.PageSize, cancellationToken);
        }

        public async Task<bool> HasUserReviewedTaskAsync(Guid userId, Guid taskId, CancellationToken cancellationToken = default)
        {
            var filterBuilder = Builders<TaskReview>.Filter;
            var filter = filterBuilder.Eq(review => review.User.UserId, userId) &
                         filterBuilder.Eq(review => review.TaskId, taskId);

            long count = await collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
            return count > 0;
        }

        public async Task<bool> TaskIdExistsAsync(Guid taskId, CancellationToken cancellationToken = default)
        {
            var filterBuilder = Builders<TaskReview>.Filter.Eq(review => review.TaskId, taskId);
            long count = await collection.CountDocumentsAsync(filterBuilder, cancellationToken: cancellationToken);
            
            return count > 0;
        }
    }
}
