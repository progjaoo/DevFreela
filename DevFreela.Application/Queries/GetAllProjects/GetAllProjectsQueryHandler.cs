using DevFreela.Application.ViewModels;
using DevFreela.Coree.InterfacesRepositorys;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
    {
        private readonly IProjectRepository _projectrepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectrepository)
        {
            _projectrepository = projectrepository;
        }

        //sempre faz com Metodo ASYNC (AWAIT)
        public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectrepository.GetAllAsync(request.Query);

            var projectViewModel = projects
                .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
                .ToList();

            return projectViewModel;
        }
    }
}
