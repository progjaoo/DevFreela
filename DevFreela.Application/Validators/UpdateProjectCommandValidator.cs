using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Commands.UpdateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20)
                .WithMessage("Titulo não pode ser vazio");
            RuleFor(p => p.Description)
                .MaximumLength(250)
                .WithMessage("Tamanho máximo de 250 caracteres");
        }
    }
}
