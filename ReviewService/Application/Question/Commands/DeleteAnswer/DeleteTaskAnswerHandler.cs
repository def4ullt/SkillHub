using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CommandInterfaces;
using Domain.Helpers;
using Domain.Interfaces;
using MediatR;

namespace Application.Question.Commands.DeleteAnswer
{
    public class DeleteTaskAnswerHandler : ICommandHandler<DeleteTaskAnswerCommand>
    {
        private ITaskQuestionRepository repo;

        public DeleteTaskAnswerHandler(ITaskQuestionRepository repo)
        {
            this.repo = repo;
        }

        public async Task<Unit> Handle(DeleteTaskAnswerCommand request, CancellationToken cancellationToken)
        {
            var question = await repo.GetByIdAsync(request.QuestionId, cancellationToken);
            if (question == null) throw new NotFoundException("TaskQuestion", request.QuestionId);

            var answerExists = question.Answers.Any(a => a.Id == request.AnswerId);
            if (!answerExists) throw new NotFoundException("TaskAnswer", request.AnswerId);

            await repo.DeleteAnswerAsync(request.QuestionId, request.AnswerId, cancellationToken);

            return Unit.Value;
        }
    }
}
