using DevFreela.Coree.InterfacesRepositorys;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFreela.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DevFreelaDbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(IProjectRepository projects, IUserRepository users, ISkillRepository skills, DevFreelaDbContext context)
        {
            Projects = projects;
            Users = users;
            _context = context;
            Skills = skills;
        }

        public IProjectRepository Projects { get; }

        public IUserRepository Users { get; }

        public ISkillRepository Skills { get; }

        public async Task BeginTransactionAsync ()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                throw new Exception();
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw ex;
            }
        }

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
