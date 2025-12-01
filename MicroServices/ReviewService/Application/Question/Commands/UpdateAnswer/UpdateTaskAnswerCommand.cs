using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.CommandInterfaces;

namespace Application.Question.Commands.UpdateAnswer
{
    public record UpdateTaskAnswerCommand(
        string QuestionId,
        string AnswerId,
        string NewText
    ) : ICommand;
}
