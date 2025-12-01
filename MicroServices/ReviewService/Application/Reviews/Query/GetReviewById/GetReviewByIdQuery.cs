using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.Reviews.Query.GetReviewById
{
    public class GetReviewByIdQuery : IRequest<TaskReview?>
    {
        public string ReviewId { get; }

        public GetReviewByIdQuery(string reviewId)
        {
            ReviewId = reviewId;
        }
    }
}
