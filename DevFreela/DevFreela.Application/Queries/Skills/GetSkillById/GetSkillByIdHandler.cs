using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.Skills.GetSkillById
{
    public class GetSkillByIdHandler : IRequestHandler<GetSkillByIdQuery, ResultViewModel<SkillViewModel>>
    {

        private readonly ISkillRepository _repository;

        public GetSkillByIdHandler(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<SkillViewModel>> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
        {
            var skill = await _repository.GetByIdAsync(request.Id);

            if (skill == null)
            {
                return ResultViewModel<SkillViewModel>.Error("Skill not found");
            }

            var model = SkillViewModel.FromEntity(skill);

            return ResultViewModel<SkillViewModel>.Success(model);

        }
    }
}
