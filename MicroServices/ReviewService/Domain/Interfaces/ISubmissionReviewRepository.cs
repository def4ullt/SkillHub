using Domain.Entities;
using Domain.Helpers;

namespace Domain.Interfaces
{
    public interface ISubmissionReviewRepository : IGenericRepository<SubmissionReview>
    {
        Task<PagedList<SubmissionReview>> GetReviewsAsync(Guid? submissionId, Guid? taskId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<bool> HasMentorReviewedSubmissionAsync(Guid mentorId, Guid submissionId, CancellationToken cancellationToken = default);
    }
}
