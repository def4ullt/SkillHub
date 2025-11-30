using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.WorkSubmissionFile;
using FluentValidation;

namespace BLL.FluentValidation.WorkSubmissionFile
{
    public class WorkSubmissionFileCreateDtoValidator : AbstractValidator<WorkSubmissionFileCreateDto>
    {
        public WorkSubmissionFileCreateDtoValidator()
        {
            RuleFor(x => x.DeliveryMethodId)
                .NotEmpty().WithMessage("DeliveryMethodId is required.");

            RuleFor(x => x.FileUrl)
                .NotEmpty().WithMessage("FileUrl is required.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("FileUrl must be a valid URL.");
        }
    }
}
