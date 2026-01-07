using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.Projects.GetAllProject
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, Result<List<ProjectItemViewModel>>>
    {
        private readonly IProjectRepository _repository;
        public GetAllProjectsHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAllAsync(request.Search, request.Page, request.Size);

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return Result<List<ProjectItemViewModel>>.Success(model);
        }
    }
}
