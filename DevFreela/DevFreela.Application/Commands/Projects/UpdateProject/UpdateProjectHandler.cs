using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Projects.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, Result>
    {
        private readonly IProjectRepository _repository;
        public UpdateProjectHandler(IProjectRepository repository)
        {
            _repository = repository;

        }
        public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.IdProject);

            if (project is null)
            {
                return Result.Failure(ErrorType.NotFound, "Projeto não encontrado.");
            }

            project.Update(request.Title, request.Description, request.TotalCost);

            await _repository.UpdateAsync(project);

            return Result.Success();
        }
    }
}
