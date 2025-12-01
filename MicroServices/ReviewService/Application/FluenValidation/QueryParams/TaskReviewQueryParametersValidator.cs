using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.QueryParams;
using FluentValidation;

namespace Application.FluenValidation.QueryParams
{
    public class TaskReviewQueryParametersValidator : AbstractValidator<TaskReviewQueryParameters>
    {
        public TaskReviewQueryParametersValidator()
        {
            When(x => x.Rating.HasValue, () =>
            {
                RuleFor(x => x.Rating.Value)
                    .InclusiveBetween(1, 5)
                    .WithMessage("Rating must be between 1 and 5 if specified");
            });
        }
    }
}
