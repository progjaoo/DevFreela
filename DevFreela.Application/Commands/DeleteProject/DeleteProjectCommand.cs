using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DevFreela.Application.Commands.DeleteCommand
{
    public class DeleteProjectCommand : IRequest<Unit>
    {
        public DeleteProjectCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; } 
    }
}
