using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CommandInterfaces;
using Domain.Helpers;
using Domain.Interfaces;
using MediatR;

namespace Application.Question.Commands.UpdateAnswer
{
    public class UpdateTaskAnswerHandler : ICommandHandler<UpdateTaskAnswerCommand>
    {
        private ITaskQuestionRepository repo;

        public UpdateTaskAnswerHandler(ITaskQuestionRepository repo)
        {
            this.repo = repo;
        }

        public async Task<Unit> Handle(UpdateTaskAnswerCommand request, CancellationToken cancellationToken)
        {
            var question = await repo.GetByIdAsync(request.QuestionId, cancellationToken);
            if (question == null)
            {
                throw new NotFoundException("TaskQuestion", request.QuestionId);
            }

            var answer = question.Answers.FirstOrDefault(a => a.Id == request.AnswerId);
            if (answer == null)
            {
                throw new NotFoundException("TaskAnswer", request.AnswerId);
            }

            answer.UpdateAnswer(request.NewText);
            await repo.UpdateAnswerAsync(request.QuestionId, answer, cancellationToken);

            return Unit.Value;
        }
    }
}
