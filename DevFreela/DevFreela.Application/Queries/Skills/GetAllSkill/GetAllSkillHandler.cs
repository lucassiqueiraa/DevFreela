using DevFreela.Application.Models;
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
        private readonly DevFreelaDbContext _context;

        public GetAllSkillHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<List<SkillViewModel>>> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
        {
            var skills = await _context.Skills.ToListAsync();

            var model = skills.Select(SkillViewModel.FromEntity).ToList();

            return ResultViewModel<List<SkillViewModel>>.Success(model);
        }
    }
}
