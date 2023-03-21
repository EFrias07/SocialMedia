using FluentValidation;
using SocialMedia.Core.DTOs;
using System;

namespace SocialMedia.Infrastructure.Validations.PostValidation
{
    public class PostValidation : AbstractValidator<PostDto>
    {
        public PostValidation()
        {
            RuleFor(a => a.Description)
                .NotNull()
                .NotEmpty()
                .Length(10,500)
                .WithMessage("La descripcion no es valida");

            RuleFor(a => a.Date)
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.Now);

            RuleFor(a => a.UserId)
                .NotNull()
                .NotEqual(0)
                .WithMessage("El usuario no puede ser el 0");
        }
    }
}
