using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Coree.DTO_s;
using DevFreela.Coree.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillDTO>>
    {
        private readonly ISkillRepository _skillRepository;
        public GetAllSkillsQueryHandler(ISkillRepository skillRepository)
        {
           _skillRepository = skillRepository;
        }

        public async Task<List<SkillDTO>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {

            return await _skillRepository.GetAll();

        }
    }
}
