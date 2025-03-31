using FluentValidation;
using Backend.Core.Application.DTOs;

namespace Backend.Core.Application.Validators
{
    public class ConfigurationDtoValidator : AbstractValidator<ConfigurationDto>
    {
        public ConfigurationDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres");

            RuleFor(x => x.WorkflowId)
                .GreaterThan(0).WithMessage("El ID del flujo de trabajo debe ser mayor que 0");

            RuleFor(x => x.EngineId)
                .GreaterThan(0).WithMessage("El ID del motor debe ser mayor que 0");

            RuleFor(x => x.RetryLimit)
                .GreaterThanOrEqualTo(0).WithMessage("El límite de reintentos no puede ser negativo")
                .LessThanOrEqualTo(10).WithMessage("El límite de reintentos no puede ser mayor a 10");
        }
    }
} 