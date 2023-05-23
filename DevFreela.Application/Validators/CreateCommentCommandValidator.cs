using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Commands.CreateCommand;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(cm => cm.Content)
                .MinimumLength(250)
                .WithMessage("O conteúdo do comentário deve ter no máximo 250 caracteres");
        }
    }
}
