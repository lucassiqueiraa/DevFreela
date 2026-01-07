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
    public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, Result<int>>
    {
        private readonly ISkillRepository _repository;
        public InsertSkillHandler(ISkillRepository repository) 
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
            // TODO: Validate the description field (if it is empty or exceeds the maximum number of characters)
            var exists = await _repository.ExistsByDescription(request.Description);
            if (exists)
            {
                return Result<int>.Failure(ErrorType.Conflict, "This skill already exists");
            }

            var skill = request.ToEntity();

            await _repository.AddSkillAsync(skill);

            return Result<int>.Success(skill.Id);
        }
    }
}
