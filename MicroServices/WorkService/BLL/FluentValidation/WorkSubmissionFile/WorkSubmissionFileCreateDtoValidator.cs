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

            
        }
    }
}
