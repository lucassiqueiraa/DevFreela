using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Users.InsertUserSkill
{
    public class InsertUserSkillHandler : IRequestHandler<InsertUserSkillCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public InsertUserSkillHandler(DevFreelaDbContext context)
        {
            _dbContext = context;
        }
        public async Task<ResultViewModel<int>> Handle(InsertUserSkillCommand request, CancellationToken cancellationToken)
        {
            var userSkill = request.SkillIds
                .Select(skillId => new UserSkill(request.UserId, skillId))
                .ToList();

            await _dbContext.UserSkills.AddRangeAsync(userSkill);
            await _dbContext.SaveChangesAsync();

            return ResultViewModel<int>.Success(request.UserId);
        }
    }
}
