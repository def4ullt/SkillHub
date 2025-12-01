using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.CommandInterfaces;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;

namespace Application.Question.Commands.AddAnswer
{
    public class AddTaskAnswerHandler : ICommandHandler<AddTaskAnswerCommand, string>
    {
        private ITaskQuestionRepository repo;

        public AddTaskAnswerHandler(ITaskQuestionRepository repo)
        {
            this.repo = repo;
        }

        public async Task<string> Handle(AddTaskAnswerCommand request, CancellationToken cancellationToken)
        {
            var question = await repo.GetByIdAsync(request.QuestionId, cancellationToken);
            if (question == null)
            {
                throw new NotFoundException("TaskQuestion", request.QuestionId);
            }

            var answer = new TaskAnswer(request.User, request.AnswerText);

            question.AddAnswer(answer);
            await repo.AddAnswerAsync(request.QuestionId, answer, cancellationToken);

            return answer.Id;
        }
    }
}
