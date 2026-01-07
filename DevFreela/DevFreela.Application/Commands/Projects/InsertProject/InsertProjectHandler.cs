using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Core.Repositories;
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
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, Result<int>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;


        public InsertProjectHandler(IProjectRepository projectRepository, IUserRepository userRepository, IMediator mediator)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _mediator = mediator;
        } 
        public async Task<Result<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var clientExists = await _userRepository.ExistsAsync(request.IdClient);

            if (!clientExists)
            {
                return Result<int>.Failure(ErrorType.NotFound, "Cliente informado não existe.");
            }

            var freelancerExists = await _userRepository.ExistsAsync(request.IdFreelancer);

            if (!freelancerExists)
            {
                return Result<int>.Failure(ErrorType.NotFound, "Freelancer informado não existe.");
            }

            var project = request.ToEntity();

            await _projectRepository.AddAsync(project);

            var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
            await _mediator.Publish(projectCreated);

            return Result<int>.Success(project.Id);
        }
    }
}
