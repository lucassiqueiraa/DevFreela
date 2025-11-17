using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
    internal class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _dbContext;

        public SkillService(DevFreelaDbContext context)
        {
            _dbContext = context;
        }
        public ResultViewModel<List<SkillViewModel>> GetAllSkill()
        {
            var skills = _dbContext.Skills.ToList();

            var models = skills
                .Select(SkillViewModel
                .FromEntity)
                .ToList();

            return ResultViewModel<List<SkillViewModel>>.Success(models);
        }

        public ResultViewModel<SkillViewModel> GetById(int id)
        {
            var skill = _dbContext.Skills.SingleOrDefault(s => s.Id == id);

            if (skill is null)
            {
                return ResultViewModel<SkillViewModel>.Error("Skill não encontrada.");
            }

            var model = SkillViewModel.FromEntity(skill);

            return ResultViewModel<SkillViewModel>.Success(model);
        }

        public ResultViewModel<int> Insert(CreateSkillInputModel model)
        {
            var skill = model.ToEntity();

            _dbContext.Skills.Add(skill);
            _dbContext.SaveChanges();

            return ResultViewModel<int>.Success(skill.Id);
        }
    }
}
