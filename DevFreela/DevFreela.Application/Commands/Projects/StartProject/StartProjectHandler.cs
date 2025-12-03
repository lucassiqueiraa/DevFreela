using DevFreela.Application.Models;
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
        private readonly DevFreelaDbContext _dbContext;
        public StartProjectHandler(DevFreelaDbContext context)
        {
            _dbContext = context;
        }
        public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project= await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado.");
            }

            project.Start();
            _dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
