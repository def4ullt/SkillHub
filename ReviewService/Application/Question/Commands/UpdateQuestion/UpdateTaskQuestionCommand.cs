using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CommandInterfaces;

namespace Application.Question.Commands.UpdateQuestion
{
    public record UpdateTaskQuestionCommand(
        string QuestionId,
        string NewText
    ) : ICommand;
}
