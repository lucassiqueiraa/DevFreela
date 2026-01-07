using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Projects.DeleteProject
{
    public class DeleteProjectCommand : IRequest<Result>
    {
        public DeleteProjectCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
