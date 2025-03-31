using FluentValidation;
using Backend.Core.Application.DTOs;

namespace Backend.Core.Application.Validators
{
    public class WorkTaskDtoValidator : AbstractValidator<WorkTaskDto>
    {
        public WorkTaskDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

            RuleFor(x => x.ConfigurationId)
                .GreaterThan(0).WithMessage("El ID de la configuración debe ser mayor que 0");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("El estado no es válido");
        }
    }
} 