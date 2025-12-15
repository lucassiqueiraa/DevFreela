using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Projects.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IMediator _mediator;


        public InsertProjectHandler(DevFreelaDbContext context, IMediator mediator)
        {
            _dbContext = context;
            _mediator = mediator;
        }
        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();

            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

            var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
            await _mediator.Publish(projectCreated);

            return ResultViewModel<int>.Success(project.Id);
        }
    }
}
