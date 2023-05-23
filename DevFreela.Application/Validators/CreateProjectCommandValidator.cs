using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20)
                .WithMessage("Tamanho máximo de título é de 20 caracteres");
            RuleFor(p => p.Description)
                .MaximumLength(250)
                .WithMessage("Tamanho máximo de descrição é de 250 caracteres");
        }
    }
}
