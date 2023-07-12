using DevFreela.Coree.InterfacesRepositorys;

namespace DevFreela.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        IProjectRepository Projects { get; }

        IUserRepository Users { get; }

        Task<int> CompleteAsync();
    }
}
