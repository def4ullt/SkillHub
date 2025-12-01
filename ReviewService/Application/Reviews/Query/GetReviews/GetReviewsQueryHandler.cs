using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using MediatR;

namespace Application.Reviews.Query.GetReviews
{
    public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, PagedList<TaskReview>>
    {
        private ITaskReviewRepository reviewRepository;

        public GetReviewsQueryHandler(ITaskReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public async Task<PagedList<TaskReview>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            return await reviewRepository.GetReviewsAsync(request.QueryParameters, cancellationToken);
        }
    }
}
