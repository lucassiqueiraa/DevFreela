using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Skills.InsertSkill
{
    public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, ResultViewModel<int>>
    {
        private readonly ISkillRepository _repostory;
        public InsertSkillHandler(ISkillRepository repository) 
        {
            _repostory = repository;
        }

        public async Task<ResultViewModel<int>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = request.ToEntity();
            await _repostory.AddSkillAsync(skill);

            return ResultViewModel<int>.Success(skill.Id);
        }
    }
}
