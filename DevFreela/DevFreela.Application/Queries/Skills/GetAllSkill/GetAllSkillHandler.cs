using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.Skills.GetAllSkill
{
    public class GetAllSkillHandler : IRequestHandler<GetAllSkillQuery, ResultViewModel<List<SkillViewModel>>>
    {
        private readonly ISkillRepository _repository;

        public GetAllSkillHandler(ISkillRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<List<SkillViewModel>>> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
        {
            var skills = await _repository.GetAllAsync();

            var model = skills.Select(SkillViewModel.FromEntity).ToList();

            return ResultViewModel<List<SkillViewModel>>.Success(model);
        }
    }
}
