using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.WorkSubmission;
using BLL.FluentValidation.WorkSubmissionFile;
using FluentValidation;

namespace BLL.FluentValidation.WorkSubmission
{

    public class WorkSubmissionCreateDtoValidator : AbstractValidator<WorkSubmissionCreateDto>
    {
        public WorkSubmissionCreateDtoValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("TaskId is required.");

            RuleFor(x => x.TaskName)
                .NotEmpty().WithMessage("TaskName is required.")
                .MaximumLength(200).WithMessage("TaskName must not exceed 200 characters.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.UserFirstName)
                .NotEmpty().WithMessage("UserFirstName is required.")
                .MaximumLength(100).WithMessage("UserFirstName must not exceed 100 characters.");

            RuleFor(x => x.UserLastName)
                .NotEmpty().WithMessage("UserLastName is required.")
                .MaximumLength(100).WithMessage("UserLastName must not exceed 100 characters.");

            RuleForEach(x => x.Files)
                .SetValidator(new WorkSubmissionFileCreateDtoValidator());
        }
    }
}
