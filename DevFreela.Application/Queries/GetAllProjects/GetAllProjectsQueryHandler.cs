using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.ViewModels;
using DevFreela.Coree.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var projects = await _projectrepository.GetAllAsync();

            var projectViewModel = projects
                .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
                .ToList();

            return projectViewModel;
        }
    }
}
