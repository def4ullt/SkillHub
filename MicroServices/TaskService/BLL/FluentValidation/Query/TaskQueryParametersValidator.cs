using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Query;
using FluentValidation;

namespace TaskService.BLL.FluentValidation.Query
{
    public class TaskQueryParametersValidator : AbstractValidator<TaskQueryParameters>
    {
        public TaskQueryParametersValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than 0.");

            RuleFor(x => x.EstimatedTimeMin)
                .GreaterThanOrEqualTo(0).When(x => x.EstimatedTimeMin.HasValue)
                .WithMessage("EstimatedTimeMin must be greater than or equal to 0.");

            RuleFor(x => x.EstimatedTimeMax)
                .GreaterThanOrEqualTo(0).When(x => x.EstimatedTimeMax.HasValue)
                .WithMessage("EstimatedTimeMax must be greater than or equal to 0.");

            When(x => x.EstimatedTimeMin.HasValue && x.EstimatedTimeMax.HasValue, () =>
            {
                RuleFor(x => x.EstimatedTimeMax.Value)
                    .GreaterThanOrEqualTo(x => x.EstimatedTimeMin.Value)
                    .WithMessage("EstimatedTimeMax must be greater than or equal to EstimatedTimeMin.");
            });

            RuleFor(x => x.XpRewardMin)
                .GreaterThanOrEqualTo(0).When(x => x.XpRewardMin.HasValue)
                .WithMessage("XpRewardMin must be greater than or equal to 0.");

            RuleFor(x => x.XpRewardMax)
                .GreaterThanOrEqualTo(0).When(x => x.XpRewardMax.HasValue)
                .WithMessage("XpRewardMax must be greater than or equal to 0.");

            When(x => x.XpRewardMin.HasValue && x.XpRewardMax.HasValue, () =>
            {
                RuleFor(x => x.XpRewardMax.Value)
                    .GreaterThanOrEqualTo(x => x.XpRewardMin.Value)
                    .WithMessage("XpRewardMax must be greater than or equal to XpRewardMin.");
            });
        }
    }
}
