using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Reviews.Commands.DeleteReview;
using FluentValidation;

namespace Application.FluenValidation.Reviews
{
    public class DeleteTaskReviewCommandValidator : AbstractValidator<DeleteTaskReviewCommand>
    {
        public DeleteTaskReviewCommandValidator()
        {
            RuleFor(x => x.ReviewId)
                .NotEmpty().WithMessage("ReviewId is required");
        }
}
}
