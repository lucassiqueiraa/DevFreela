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
    public class InsertUserSkillHandler : IRequestHandler<InsertUserSkillCommand, Result<int>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISkillRepository _skillRepository;

        public InsertUserSkillHandler(IUserRepository userRepository, ISkillRepository skillRepository)
        {
            _userRepository = userRepository;
            _skillRepository = skillRepository;
        }
        public async Task<Result<int>> Handle(InsertUserSkillCommand request, CancellationToken cancellationToken)
        {
           
            if (request.SkillIds == null || !request.SkillIds.Any())
            {
                 return Result<int>.Failure(ErrorType.Validation, "At least one skill must be provided");
            }


            var exists = await _userRepository.ExistsAsync(request.UserId);

            if(!exists)
            {
                return Result<int>.Failure(ErrorType.NotFound, "User does not exist");
            }

            var distinctSkillsRequested = request.SkillIds.Distinct().Count();

            var foundSkillsCount = await _skillRepository.GetExistingSkillsCountAsync(request.SkillIds);

            if (foundSkillsCount != distinctSkillsRequested)
            {
                return Result<int>.Failure(ErrorType.Validation, "Invalid Skill ID provided.");
            }

            //TODO: check if has skills duplicated
            var userSkill = request.SkillIds
                .Distinct()
                .Select(skillId => new UserSkill(request.UserId, skillId))
                .ToList();

            await _userRepository.AddSkillsAsync(userSkill);

            return Result<int>.Success(request.UserId);
        }
    }
}
