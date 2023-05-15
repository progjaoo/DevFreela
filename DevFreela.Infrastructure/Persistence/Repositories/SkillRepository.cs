using Dapper;
using DevFreela.Coree.DTO_s;
using DevFreela.Coree.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly string _connectionString;

        public SkillRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
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
