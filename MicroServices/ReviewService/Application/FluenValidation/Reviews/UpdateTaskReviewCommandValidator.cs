using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Reviews.Commands.UpdateReview;
using FluentValidation;

namespace Application.FluenValidation.Reviews
{
    public class UpdateTaskReviewCommandValidator : AbstractValidator<UpdateTaskReviewCommand>
    {
        public UpdateTaskReviewCommandValidator()
        {
            RuleFor(x => x.ReviewId)
                .NotEmpty().WithMessage("ReviewId is required");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comment is required");
        }
    }
}
