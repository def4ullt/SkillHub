using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CommandInterfaces;
using Domain.ValueObjects;

namespace Application.Question.Commands.CreateQuestion
{
    public record CreateTaskQuestionCommand(
        Guid TaskId,
        string QuestionText,
        UserInformation User
    ) : ICommand<string>;
}
