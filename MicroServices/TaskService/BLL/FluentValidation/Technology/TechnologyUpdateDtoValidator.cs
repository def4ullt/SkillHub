using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Technology;
using FluentValidation;

namespace BLL.FluentValidation.Technology
{
    public class TechnologyUpdateDtoValidator : AbstractValidator<TechnologyUpdateDto>
    {
        public TechnologyUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Technology name is required.")
                .MaximumLength(100)
                .WithMessage("Technology name cannot exceed 100 characters.");
        }
    }
}
