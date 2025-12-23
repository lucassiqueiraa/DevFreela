using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.Projects.GetProjectById
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly IProjectRepository _repository;
        public GetProjectByIdHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetDetailsByIdAsync(request.Id);

            if (project is null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Projeto não existe.");
            }

            var projectViewModel = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Success(projectViewModel);


        }
    }
}
