using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Question.Commands.CreateQuestion;
using FluentValidation;

namespace Application.FluenValidation.Question
{
    public class CreateTaskQuestionCommandValidator : AbstractValidator<CreateTaskQuestionCommand>
    {
        public CreateTaskQuestionCommandValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("TaskId is required");

            RuleFor(x => x.QuestionText)
                .NotEmpty().WithMessage("QuestionText is required");

            RuleFor(x => x.User)
                .NotNull().WithMessage("User information is required");
        }
    }
}
