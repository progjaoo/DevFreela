using DevFreela.Coree.Entities;
using DevFreela.Coree.Models;

namespace DevFreela.Coree.InterfacesRepositorys
{
    public interface IProjectRepository
    {
        Task<PaginationResult<Project>> GetAllAsync(string query, int page = 1); //lista para retorn
        Task<Project> GetByIdAsync(int id); //só project pq pega pelo id
        Task<Project> GetDetailsByIdAsync(int id);
        Task AddASync(Project project);
        Task AddCommentAsync(ProjectComment projectcomment);
        Task StartAsync(Project project);
        Task SaveChangesAsync();
    }
}
