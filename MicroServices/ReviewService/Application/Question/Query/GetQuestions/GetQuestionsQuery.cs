using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.QueryParams;
using Domain.Helpers;
using MediatR;

namespace Application.Question.Query.GetQuestions
{
    public class GetQuestionsQuery : IRequest<PagedList<TaskQuestion>>
    {
        public TaskQuestionQueryParameters QueryParameters { get; }

        public GetQuestionsQuery(TaskQuestionQueryParameters queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }
}
