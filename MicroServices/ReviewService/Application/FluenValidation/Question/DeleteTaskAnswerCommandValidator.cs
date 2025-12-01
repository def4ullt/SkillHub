using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Question.Commands.DeleteAnswer;
using FluentValidation;

namespace Application.FluenValidation.Question
{
    public class DeleteTaskAnswerCommandValidator : AbstractValidator<DeleteTaskAnswerCommand>
    {
        public DeleteTaskAnswerCommandValidator()
        {
            RuleFor(x => x.QuestionId)
                .NotEmpty().WithMessage("QuestionId is required");

            RuleFor(x => x.AnswerId)
                .NotEmpty().WithMessage("AnswerId is required");
        }
    }
}
