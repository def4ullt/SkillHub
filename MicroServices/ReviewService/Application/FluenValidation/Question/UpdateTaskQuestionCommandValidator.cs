using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Question.Commands.UpdateQuestion;
using FluentValidation;

namespace Application.FluenValidation.Question
{
    public class UpdateTaskQuestionCommandValidator : AbstractValidator<UpdateTaskQuestionCommand>
    {
        public UpdateTaskQuestionCommandValidator()
        {
            RuleFor(x => x.QuestionId)
                .NotEmpty().WithMessage("QuestionId is required");

            RuleFor(x => x.NewText)
                .NotEmpty().WithMessage("NewText is required");
        }
    }
}
