using DevFreela.Application.Models;
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
        private readonly DevFreelaDbContext _context;
        public GetSkillByIdHandler(DevFreelaDbContext context) 
        { 
            _context = context;
        }

        public async Task<ResultViewModel<SkillViewModel>> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
        {
            var skill = await _context.Skills.FindAsync(request.Id);

            if (skill == null)
            {
                return ResultViewModel<SkillViewModel>.Error("Skill not found");
            }

            var model = SkillViewModel.FromEntity(skill);

            return ResultViewModel<SkillViewModel>.Success(model);

        }
    }
}
