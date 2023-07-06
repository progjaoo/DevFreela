using DevFreela.Coree.Entities;
using DevFreela.Coree.InterfacesRepositorys;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        //await delega a operacao de entra/sai do BD e a thread fica livre...
        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Title, request.Description, request.IdCliente, request.IdFreelancer, request.TotalCost);
            
            await _projectRepository.AddASync(project);
            
            return project.Id;
        }
    }
}
