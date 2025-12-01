using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Question.Commands.AddAnswer;
using FluentValidation;

namespace Application.FluenValidation.Question
{
    public class AddTaskAnswerCommandValidator : AbstractValidator<AddTaskAnswerCommand>
    {
        public AddTaskAnswerCommandValidator()
        {
            RuleFor(x => x.QuestionId)
                .NotEmpty().WithMessage("QuestionId is required");

            RuleFor(x => x.AnswerText)
                .NotEmpty().WithMessage("AnswerText is required");

            RuleFor(x => x.User)
                .NotNull().WithMessage("User information is required");
        }
    }
}
