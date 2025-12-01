using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.CommandInterfaces;
using Domain.ValueObjects;

namespace Application.Question.Commands.AddAnswer
{
    public record AddTaskAnswerCommand(
        string QuestionId,
        string AnswerText,
        UserInformation User
    ) : ICommand<string>;
}
