using DevFreela.Coree.Entities;

namespace DevFreela.Coree.InterfacesRepositorys
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync(string query); //lista para retorn
        Task<Project> GetByIdAsync(int id); //só project pq pega pelo id
        Task<Project> GetDetailsByIdAsync(int id);
        Task AddASync(Project project);
        Task AddCommentAsync(ProjectComment projectcomment);
        Task SaveChangesAsync();
        Task StartAsync(Project project);
    }
}
