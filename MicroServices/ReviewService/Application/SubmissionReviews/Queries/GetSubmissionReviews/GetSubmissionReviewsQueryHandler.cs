using Application.Interfaces.QueriesInterfaces;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;

namespace Application.SubmissionReviews.Queries.GetSubmissionReviews
{
    public class GetSubmissionReviewsQueryHandler : IQueryHandler<GetSubmissionReviewsQuery, PagedList<SubmissionReview>>
    {
        private readonly ISubmissionReviewRepository repo;

        public GetSubmissionReviewsQueryHandler(ISubmissionReviewRepository repo)
        {
            this.repo = repo;
        }

        public async Task<PagedList<SubmissionReview>> Handle(GetSubmissionReviewsQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetReviewsAsync(request.SubmissionId, request.TaskId, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
