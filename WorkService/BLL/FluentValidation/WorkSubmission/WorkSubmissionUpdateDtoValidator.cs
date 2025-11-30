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
    public class WorkSubmissionUpdateDtoValidator : AbstractValidator<WorkSubmissionUpdateDto>
    {
        public WorkSubmissionUpdateDtoValidator()
        {
            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage("StatusId is required.");

            RuleForEach(x => x.Files)
                .SetValidator(new WorkSubmissionFileUpdateDtoValidator());
        }
    }
}
