using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Coree.Entities;
using DevFreela.Coree.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbcontext;

        public UserRepository(DevFreelaDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbcontext.Users.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(User user)
        {
            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();
        }

    }
}
