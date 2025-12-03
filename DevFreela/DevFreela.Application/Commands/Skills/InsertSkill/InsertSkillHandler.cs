using DevFreela.Application.Models;
using DevFreela.Core.Entities;
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
        private readonly DevFreelaDbContext _context;
        public InsertSkillHandler(DevFreelaDbContext context) 
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = request.ToEntity();
            await _context.AddAsync(skill);
            await _context.SaveChangesAsync();

            return ResultViewModel<int>.Success(skill.Id);
        }
    }
}
