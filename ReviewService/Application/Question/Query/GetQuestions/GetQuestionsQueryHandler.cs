using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using MediatR;

namespace Application.Question.Query.GetQuestions
{
    public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, PagedList<TaskQuestion>>
    {
        private ITaskQuestionRepository questionRepository;

        public GetQuestionsQueryHandler(ITaskQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public async Task<PagedList<TaskQuestion>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            return await questionRepository.GetQuestionsAsync(request.QueryParameters, cancellationToken);
        }
    }
}
