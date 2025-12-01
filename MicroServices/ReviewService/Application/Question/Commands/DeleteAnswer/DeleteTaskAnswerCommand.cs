using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.CommandInterfaces;

namespace Application.Question.Commands.DeleteAnswer
{
    public record DeleteTaskAnswerCommand(
        string QuestionId,
        string AnswerId
    ) : ICommand;
}
