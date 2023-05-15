using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Coree.DTO_s;

namespace DevFreela.Coree.Repositories
{
    public interface ISkillRepository
    {
        Task<List<SkillDTO>>GetAll(); 
    }

}
