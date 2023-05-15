using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{

    public class CreateProjectCommand : IRequest<int>
        //busca na program todos comandos que implementa irequest
        //associa ao IrequestHandler
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int IdCliente { get; set; }

        public int IdFreelancer { get; set; }

        public decimal TotalCost { get; set; }
    }
}
