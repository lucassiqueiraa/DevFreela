using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.Projects.CompleteProject
{
    public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, Result>
    {
        private readonly IProjectRepository _repository;
        public CompleteProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Id);

            if (project is null)
            {
                return Result.Failure(ErrorType.NotFound, "Projeto não encontrado.");
            }

            project.Complete();
            await _repository.UpdateAsync(project);

            return Result.Success();
        }
    }
}
