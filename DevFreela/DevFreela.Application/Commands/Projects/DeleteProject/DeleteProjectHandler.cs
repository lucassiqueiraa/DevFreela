using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.Projects.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;
        public DeleteProjectHandler(DevFreelaDbContext context)
        {
            _dbContext = context;
        }
        public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id); //project trackeado

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado.");
            }

            project.SetAsDeleted();
            _dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}