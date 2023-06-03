using System.Text.RegularExpressions;
using DevFreela.Application.Commands.CreateUser;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("E-mail inválido");

            RuleFor(u => u.Password)
                .Must(ValidPassword)
                .WithMessage("A senha deve conter pelo menos, 8 caracteres, um número, uma letra maiuscula, uma letra minuscula e um caractere especial");

            RuleFor(u => u.FullName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome é obrigatório");
        }

        public bool ValidPassword(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
            return regex.IsMatch(password);
        }
    }
}
