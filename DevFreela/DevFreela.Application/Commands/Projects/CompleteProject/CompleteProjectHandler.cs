using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.Projects.CompleteProject
{
    public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;
        public CompleteProjectHandler(DevFreelaDbContext context)
        {
            _dbContext = context;
        }
        public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id); //project trackeado

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado.");
            }

            project.Complete();
            _dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
