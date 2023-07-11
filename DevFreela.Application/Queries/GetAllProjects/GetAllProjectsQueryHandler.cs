using DevFreela.Application.ViewModels;
using DevFreela.Coree.InterfacesRepositorys;
using DevFreela.Coree.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, PaginationResult<ProjectViewModel>>
    {
        private readonly IProjectRepository _projectrepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectrepository)
        {
            _projectrepository = projectrepository;
        }

        //sempre faz com Metodo ASYNC (AWAIT)
        public async Task<PaginationResult<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var paginationProjects = await _projectrepository.GetAllAsync(request.Query, request.Page);

            var projectViewModel = paginationProjects
                .Data
                .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
                .ToList();

            var paginationProjectsViewModel = new PaginationResult<ProjectViewModel>(
                paginationProjects.Page,
                paginationProjects.TotalPages,
                paginationProjects.PageSize,
                paginationProjects.ItemsCount,
                projectViewModel                
                );

            return paginationProjectsViewModel;
        }
    }
}
