using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.CommandInterfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Question.Commands.CreateQuestion
{
    public class CreateTaskQuestionHandler : ICommandHandler<CreateTaskQuestionCommand, string>
    {
        private ITaskQuestionRepository repo;

        public CreateTaskQuestionHandler(ITaskQuestionRepository repo)
        {
            this.repo = repo;
        }

        public async Task<string> Handle(CreateTaskQuestionCommand request, CancellationToken cancellationToken)
        {
            TaskQuestion question = new TaskQuestion(request.TaskId, request.User, request.QuestionText);

            await repo.AddAsync(question, cancellationToken);

            return question.Id;
        }
    }
}
