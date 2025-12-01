using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Reviews.Commands.CreateReview;
using FluentValidation;

namespace Application.FluenValidation.Reviews
{
    public class CreateTaskReviewCommandValidator : AbstractValidator<CreateTaskReviewCommand>
    {
        public CreateTaskReviewCommandValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("TaskId is required");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comment is required");

            RuleFor(x => x.User)
                .NotNull().WithMessage("User information is required");
        }
    }
}
