using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Question.Commands.DeleteQuestion;
using FluentValidation;

namespace Application.FluenValidation.Question
{
    public class DeleteTaskQuestionCommandValidator : AbstractValidator<DeleteTaskQuestionCommand>
    {
        public DeleteTaskQuestionCommandValidator()
        {
            RuleFor(x => x.QuestionId)
                .NotEmpty().WithMessage("QuestionId is required");
        }
    }
}
