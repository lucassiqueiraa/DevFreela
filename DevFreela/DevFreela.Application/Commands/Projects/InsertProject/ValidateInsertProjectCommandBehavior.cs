using DevFreela.Application.Models;
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
    public class ValidateInsertProjectCommandBehavior : IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        public ValidateInsertProjectCommandBehavior(DevFreelaDbContext context)
        {
            _context = context;

        }
        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            var clientExists = await _context.Users.FindAsync(request.IdClient);
            var freelancerExists = await _context.Users.FindAsync(request.IdFreelancer);

             if (clientExists == null || freelancerExists == null)
            {
                return ResultViewModel<int>.Error("Client or Freelancer not found");
            }


             return await next();
        }
    }
}
