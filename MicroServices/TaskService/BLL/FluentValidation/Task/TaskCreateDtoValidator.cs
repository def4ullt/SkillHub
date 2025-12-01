using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Task;
using FluentValidation;

namespace BLL.FluentValidation.Task
{
    public class TaskCreateDtoValidator : AbstractValidator<TaskCreateDto>
    {
        public TaskCreateDtoValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");

            RuleFor(t => t.EstimatedTimeMinutes)
                .GreaterThan(0).WithMessage("Estimated time must be greater than 0");

            RuleFor(t => t.XpReward)
                .GreaterThanOrEqualTo(0).WithMessage("XP reward must be non-negative");

            RuleFor(t => t.TechnologyIds)
                .NotNull().WithMessage("TechnologyIds cannot be null")
                .Must(list => list.Count > 0).WithMessage("At least one TechnologyId must be provided");

            RuleFor(t => t.TagIds)
                .NotNull().WithMessage("TagIds cannot be null")
                .Must(list => list.Count > 0).WithMessage("At least one TagId must be provided");
        }
    }
}
