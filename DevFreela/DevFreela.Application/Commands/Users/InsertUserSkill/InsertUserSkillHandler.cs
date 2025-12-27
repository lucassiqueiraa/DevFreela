using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
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
        private readonly IUserRepository _repository;

        public InsertUserSkillHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<int>> Handle(InsertUserSkillCommand request, CancellationToken cancellationToken)
        {
            var userSkill = request.SkillIds
                .Select(skillId => new UserSkill(request.UserId, skillId))
                .ToList();

            await _repository.AddSkillsAsync(userSkill);

            return ResultViewModel<int>.Success(request.UserId);
        }
    }
}
