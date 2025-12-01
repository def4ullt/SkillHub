using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Helpers;
using FluentValidation;

namespace WorkService.BLL.FluentValidation.Query
{
    public class WorkSubmissionQueryParamsValidator : AbstractValidator<WorkSubmissionQueryParams>
    {
        public WorkSubmissionQueryParamsValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("PageNumber must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("PageSize must be greater than 0.");
        }
    }
}
