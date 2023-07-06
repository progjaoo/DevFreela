using Dapper;
using DevFreela.Coree.Entities;
using DevFreela.Coree.InterfacesRepositorys;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbcontext;
        private readonly string _connectionString;

        public ProjectRepository(DevFreelaDbContext dbcontext, IConfiguration configuration)
        {
            _dbcontext = dbcontext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbcontext.Projects.ToListAsync();
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
            await _dbcontext.SaveChangesAsync();
        }

        public async Task AddCommentAsync(ProjectComment projectcomment)
        {
            await _dbcontext.ProjectComments.AddAsync(projectcomment);
            await _dbcontext.SaveChangesAsync();
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
