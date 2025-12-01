using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.CommandInterfaces;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using MediatR;

namespace Application.Question.Commands.UpdateQuestion
{
    public class UpdateTaskQuestionHandler : ICommandHandler<UpdateTaskQuestionCommand>
    {
        private ITaskQuestionRepository repo;

        public UpdateTaskQuestionHandler(ITaskQuestionRepository repo)
        {
            this.repo = repo;
        }

        public async Task<Unit> Handle(UpdateTaskQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await repo.GetByIdAsync(request.QuestionId, cancellationToken);

            if (question == null)
            {
                throw new NotFoundException("TaskQuestion", request.QuestionId);
            }

            question.UpdateQuestion(request.NewText);

            await repo.UpdateQuestionAsync(question, cancellationToken);

            return Unit.Value;
        }
    }
}
