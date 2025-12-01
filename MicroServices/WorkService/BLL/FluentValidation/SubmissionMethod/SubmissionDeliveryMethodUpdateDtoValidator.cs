using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.DeliveryMethod;
using FluentValidation;

namespace BLL.FluentValidation.SubmissionMethod
{
    public class SubmissionDeliveryMethodUpdateDtoValidator : AbstractValidator<SubmissionDeliveryMethodUpdateDto>
    {
        public SubmissionDeliveryMethodUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        }
    }
}
