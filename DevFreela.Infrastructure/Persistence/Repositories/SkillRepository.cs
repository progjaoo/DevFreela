using Dapper;
using DevFreela.Coree.DTO_s;
using DevFreela.Coree.Entities;
using DevFreela.Coree.InterfacesRepositorys;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly string _connectionString;
        private readonly DevFreelaDbContext _dbcontext;

        public SkillRepository(DevFreelaDbContext dbcontext, IConfiguration configuration)
        {
            _dbcontext = dbcontext; 
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task AddSkillFromProject(Project project)
        {
            //app com xamarin de marketplace
            var words = project.Description.Split(' ');
            var length = words.Length;

            var skill = $"{project.Id} - {words[length - 1]}";
            // 1-marketplace
            await _dbcontext.Skills.AddAsync(new Skill(skill));
        }

        public async Task<List<SkillDTO>> GetAll()
        {
            //dapper
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var script = "SELECT Id, Description FROM Skills";

                var skills = await sqlConnection.QueryAsync<SkillDTO>(script);

                return skills.ToList();
            }
            //com EF Core só injetar o DevFreela e usar essa consulta

            //var skills = _dbcontext.Skills;

            //    var skillsViewModel = skills
            //        .Select(s => new SkillViewModel(s.Id, s.Description)).ToList();

            //    return skillsViewModel;
        }
    }
}
