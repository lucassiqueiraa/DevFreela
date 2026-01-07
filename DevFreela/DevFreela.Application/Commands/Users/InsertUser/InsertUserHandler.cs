using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Users.InsertUser
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, Result<int>>
    {
        private readonly DevFreelaDbContext _dbContext;
        public InsertUserHandler(DevFreelaDbContext context) 
        { 
            _dbContext = context;
        }
        public async Task<Result<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToEntity();

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return Result<int>.Success(user.Id);

        }
    }
}
