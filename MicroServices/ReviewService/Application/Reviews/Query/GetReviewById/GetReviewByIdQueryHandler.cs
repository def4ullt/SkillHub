using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using MediatR;

namespace Application.Reviews.Query.GetReviewById
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, TaskReview?>
    {
        private ITaskReviewRepository reviewRepository;

        public GetReviewByIdQueryHandler(ITaskReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public async Task<TaskReview?> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            TaskReview? review = await reviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);
            if (review == null) throw new NotFoundException($"Review with ID {request.ReviewId} not found!");

            return review;
        }
    }
}
