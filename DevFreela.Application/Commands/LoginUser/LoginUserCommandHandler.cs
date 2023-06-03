using DevFreela.Application.ViewModels;
using DevFreela.Coree.Repositories;
using DevFreela.Coree.Services;
using MediatR;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            //recebe a senha e utiliza o algoritmo e cria o hash
            var passwordHash = _authService.ComputeSha256Hash(request.Password);
            
            //busca no BD um user que tenha o email e senha buscada..
            var user = await _userRepository.GetUserByEmailByPasswordAsync(request.Email, passwordHash);

            //se nao tiver, if com erro
            if(user == null)
            {
                return null;
            }
            //se existe? gera token usando os dados do usuário.
            var token = _authService.GenerateJwtToken(user.Email, user.Role);
            
            return new LoginUserViewModel(user.Email, token);       
        }
    }
}
