using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Projects.StartProject
{
    public class StartProjectCommand : IRequest<ResultViewModel>
    {
        public StartProjectCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }

    }
}
