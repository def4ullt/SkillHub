using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using MediatR;

namespace Application.Question.Query.GetQuestionById
{
    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, TaskQuestion?>
    {
        private ITaskQuestionRepository questionRepository;

        public GetQuestionByIdQueryHandler(ITaskQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public async Task<TaskQuestion?> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            TaskQuestion? question = await questionRepository.GetByIdAsync(request.QuestionId, cancellationToken);
            if (question == null) throw new NotFoundException("TaskQuestion", request.QuestionId);

            return question;
        }
    }
}
