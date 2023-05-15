using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DevFreela.Application.Commands.CreateCommand
{
    public class CreateCommentCommand : IRequest<Unit>
    {
        public string Content { get; set; }

        public int IdUser { get; set; }

        public int IdProject { get; set; }
    }
}
