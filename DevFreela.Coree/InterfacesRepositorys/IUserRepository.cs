using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Coree.Entities;

namespace DevFreela.Coree.InterfacesRepositorys
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetUserByEmailByPasswordAsync(string email, string passwordHash);
    }
}
