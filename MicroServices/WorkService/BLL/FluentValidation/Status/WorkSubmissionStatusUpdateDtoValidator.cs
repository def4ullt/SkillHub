using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.WorkSubmissionStatus;
using FluentValidation;

namespace BLL.FluentValidation.Status
{
    public class WorkSubmissionStatusUpdateDtoValidator : AbstractValidator<WorkSubmissionStatusUpdateDto>
    {
        public WorkSubmissionStatusUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        }
    }
}
