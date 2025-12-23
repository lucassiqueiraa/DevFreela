using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace DevFreela.Application.Commands.Projects.InsertComment
{
    public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;

        public InsertCommentHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.ExistsAsync(request.IdProject);

            if (!exists)
            {
                return ResultViewModel.Error("Projeto não encontrado.");
            }

            var comment = request.ToEntity();

            await _repository.AddCommentAsync(comment);

            return ResultViewModel.Success();
        }
    }
}
