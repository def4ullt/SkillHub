using Application.Interfaces.QueriesInterfaces;
using Domain.Entities;
using Domain.Helpers;

namespace Application.SubmissionReviews.Queries.GetSubmissionReviews
{
    public record GetSubmissionReviewsQuery(Guid? SubmissionId, Guid? TaskId, int PageNumber, int PageSize)
        : IQuery<PagedList<SubmissionReview>>;
}
