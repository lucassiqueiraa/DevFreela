using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.Projects.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<Result<ProjectViewModel>>
    {
        public GetProjectByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
