using DevFreela.Coree.Entities;
using DevFreela.Coree.InterfacesRepositorys;
using MediatR;

namespace DevFreela.Application.Commands.CreateCommand
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, Unit>
    {
        private readonly IProjectRepository? _projectRepository;

        public CreateCommentHandler(IProjectRepository? projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _projectRepository.AddCommentAsync(comment);

            return Unit.Value; //diz que não tem retorno no mediator
        }
    }
}
