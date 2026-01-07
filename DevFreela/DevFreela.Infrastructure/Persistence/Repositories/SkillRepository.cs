using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _context;

        public SkillRepository(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddSkillAsync(Skill skill)
        {
            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();

            return skill.Id;
        }

        public async Task<bool> ExistsByDescription(string description)
        {
            return await _context.Skills.AnyAsync(s => s.Description == description);
        }

        public async Task<List<Skill>> GetAllAsync()
        {
            var skills = await _context.Skills.ToListAsync();
            return skills;
        }

        public async Task<Skill?> GetByIdAsync(int id)
        {
            return await _context.Skills.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> GetExistingSkillsCountAsync(int[] skillIds)
        {
            return await _context.Skills.CountAsync(s => skillIds.Contains(s.Id));

            //return await _context.Skills
            //    .Where(s => skillIds.Contains(s.Id))
            //    .CountAsync();
        }
    }
}
