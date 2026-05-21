using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Infrastructure.Context;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class SubmissionReviewRepository : GenericRepository<SubmissionReview>, ISubmissionReviewRepository
    {
        public SubmissionReviewRepository(MongoDbContext context, IClientSessionHandle? session = null)
            : base(context, session) { }

        public async Task<PagedList<SubmissionReview>> GetReviewsAsync(Guid? submissionId, Guid? taskId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var filter = Builders<SubmissionReview>.Filter.Empty;

            if (submissionId.HasValue)
                filter &= Builders<SubmissionReview>.Filter.Eq(r => r.SubmissionId, submissionId.Value);

            if (taskId.HasValue)
                filter &= Builders<SubmissionReview>.Filter.Eq(r => r.TaskId, taskId.Value);

            var findFluent = collection.Find(filter);
            return await PagedList<SubmissionReview>.ToPagedListAsync(findFluent, pageNumber, pageSize, cancellationToken);
        }

        public async Task<bool> HasMentorReviewedSubmissionAsync(Guid mentorId, Guid submissionId, CancellationToken cancellationToken = default)
        {
            var filter = Builders<SubmissionReview>.Filter.Eq(r => r.Mentor.UserId, mentorId)
                       & Builders<SubmissionReview>.Filter.Eq(r => r.SubmissionId, submissionId);

            long count = await collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
            return count > 0;
        }
    }
}
