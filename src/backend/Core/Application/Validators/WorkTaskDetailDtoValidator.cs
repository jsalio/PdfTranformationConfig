using FluentValidation;
using Backend.Core.Application.DTOs;

namespace Backend.Core.Application.Validators
{
    public class WorkTaskDetailDtoValidator : AbstractValidator<WorkTaskDetailDto>
    {
        public WorkTaskDetailDtoValidator()
        {
            RuleFor(x => x.WorkTaskId)
                .GreaterThan(0).WithMessage("El ID de la tarea de trabajo debe ser mayor que 0");

            RuleFor(x => x.DocumentId)
                .NotEmpty().WithMessage("El ID del documento es obligatorio");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("El estado no es válido");
        }
    }
} 