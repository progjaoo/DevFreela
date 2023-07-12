using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Coree.InterfacesRepositorys;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevFreela.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DevFreelaDbContext _context;

        public UnitOfWork(IProjectRepository projects, IUserRepository users, DevFreelaDbContext context)
        {
            Projects = projects;
            Users = users;
            _context = context;
        }

        public IProjectRepository Projects { get; }

        public IUserRepository Users { get; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();  

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
