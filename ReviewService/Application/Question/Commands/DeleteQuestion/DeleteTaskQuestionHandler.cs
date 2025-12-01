using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CommandInterfaces;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using MediatR;

namespace Application.Question.Commands.DeleteQuestion
{
    public class DeleteTaskQuestionHandler : ICommandHandler<DeleteTaskQuestionCommand>
    {
        private ITaskQuestionRepository repo;

        public DeleteTaskQuestionHandler(ITaskQuestionRepository repo)
        {
            this.repo = repo;
        }

        public async Task<Unit> Handle(DeleteTaskQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await repo.GetByIdAsync(request.QuestionId, cancellationToken);
            if (question == null) throw new NotFoundException("TaskQuestion", request.QuestionId);

            await repo.DeleteAsync(request.QuestionId, cancellationToken);

            return Unit.Value;
        }
    }
}
