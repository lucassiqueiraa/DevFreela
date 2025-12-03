using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Projects.CompleteProject
{
    public class CompleteProjectCommand : IRequest<ResultViewModel>
    {

        public CompleteProjectCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
