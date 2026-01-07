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
        public Result<List<SkillViewModel>> GetAllSkill()
        {
            var skills = _dbContext.Skills.ToList();

            var models = skills
                .Select(SkillViewModel
                .FromEntity)
                .ToList();

            return Result<List<SkillViewModel>>.Success(models);
        }

        public Result<SkillViewModel> GetById(int id)
        {
            var skill = _dbContext.Skills.SingleOrDefault(s => s.Id == id);

            if (skill is null)
            {
                return Result<SkillViewModel>.Failure(ErrorType.NotFound, "skill não encontrada.");
            }

            var model = SkillViewModel.FromEntity(skill);

            return Result<SkillViewModel>.Success(model);
        }

        public Result<int> Insert(CreateSkillInputModel model)
        {
            var skill = model.ToEntity();

            _dbContext.Skills.Add(skill);
            _dbContext.SaveChanges();

            return Result<int>.Success(skill.Id);
        }
    }
}
