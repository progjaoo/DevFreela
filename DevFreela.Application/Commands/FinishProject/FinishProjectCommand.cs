﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommand : IRequest<bool>
    {
        public int Id { get; set; } 

        public string CreditCarNumber { get; set; }

        public string Cvv { get; set; }

        public string ExpiresAt { get; set; }

        public string FullName { get; set; }
    }
}
