using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.QueryParams;
using Domain.Helpers;
using MediatR;

namespace Application.Reviews.Query.GetReviews
{
    public class GetReviewsQuery : IRequest<PagedList<TaskReview>>
    {
        public TaskReviewQueryParameters QueryParameters { get; }

        public GetReviewsQuery(TaskReviewQueryParameters queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }
}
