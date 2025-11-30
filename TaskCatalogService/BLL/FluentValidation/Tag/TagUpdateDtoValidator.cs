using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Tag;
using FluentValidation;

namespace BLL.FluentValidation.Tag
{
    public class TagUpdateDtoValidator : AbstractValidator<TagUpdateDto>
    {
        public TagUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tag name is required.")
                .MaximumLength(50)
                .WithMessage("Tag name cannot exceed 50 characters.");
        }
    }
}
