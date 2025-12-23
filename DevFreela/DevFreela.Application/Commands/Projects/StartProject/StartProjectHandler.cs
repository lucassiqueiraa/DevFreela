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

namespace DevFreela.Application.Commands.Projects.StartProject
{
    public class StartProjectHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        public StartProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project= await _repository.GetByIdAsync(request.Id);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado.");
            }

            project.Start();
            await _repository.UpdateAsync(project);

            return ResultViewModel.Success();
        }
    }
}
