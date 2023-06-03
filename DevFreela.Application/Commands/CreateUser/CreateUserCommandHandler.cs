using DevFreela.Coree.Entities;
using DevFreela.Coree.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly DevFreelaDbContext _dbcontext;
        private readonly IAuthService _authService;
        public CreateUserCommandHandler(DevFreelaDbContext dbcontext, IAuthService authService)
        {
            _dbcontext = dbcontext;
            _authService = authService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //substituiu do var user = new user, para o método lá em AuthService...
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            var user = new User(request.FullName, request.Email, request.BirthDate, passwordHash, request.Role);

            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();

            return user.Id;
        }
    }
}
