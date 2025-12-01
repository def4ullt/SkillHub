using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Question.Commands.UpdateAnswer;
using FluentValidation;

namespace Application.FluenValidation.Question
{
    public class UpdateTaskAnswerCommandValidator : AbstractValidator<UpdateTaskAnswerCommand>
    {
        public UpdateTaskAnswerCommandValidator()
        {
            RuleFor(x => x.QuestionId)
                .NotEmpty().WithMessage("QuestionId is required");

            RuleFor(x => x.AnswerId)
                .NotEmpty().WithMessage("AnswerId is required");

            RuleFor(x => x.NewText)
                .NotEmpty().WithMessage("NewText is required");
        }
    }
}
