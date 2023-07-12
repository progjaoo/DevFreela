using Dapper;
using DevFreela.Coree.Entities;
using DevFreela.Coree.InterfacesRepositorys;
using DevFreela.Coree.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbcontext;
        private readonly string _connectionString;
        private const int PAGE_SIZE = 2;
        public ProjectRepository(DevFreelaDbContext dbcontext, IConfiguration configuration)
        {
            _dbcontext = dbcontext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<PaginationResult<Project>> GetAllAsync(string query, int page)
        {
            IQueryable<Project> projects = _dbcontext.Projects;

            if(!string.IsNullOrWhiteSpace(query))
            {
                projects = projects.Where(
                    p => p.Title.Contains(query) || p.Description.Contains(query));
            }
            return await projects.GetPaged<Project>(page, PAGE_SIZE);
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _dbcontext.Projects.SingleOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Project> GetDetailsByIdAsync(int id)
        {
            return await _dbcontext.Projects
                .Include(p => p.Cliente)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddASync(Project project)
        {
            await _dbcontext.Projects.AddAsync(project);
        }

        public async Task AddCommentAsync(ProjectComment projectcomment)
        {
            await _dbcontext.ProjectComments.AddAsync(projectcomment);
        }

        public async Task StartAsync(Project project)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedat WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = project.StartedAt, project.Id });
            }

        }

        public async Task SaveChangesAsync()
        {
            await _dbcontext.SaveChangesAsync();
        }
    }
}
