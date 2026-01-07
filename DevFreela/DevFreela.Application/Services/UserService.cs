using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;
        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Result<UserViewModel> GetById(int id)
        {
            var user = _dbContext.Users
                .Include(u => u.Skills)
                    .ThenInclude(us => us.Skill)
                .SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return Result<UserViewModel>.Failure(ErrorType.NotFound, "Usuário não encontrado.");
            }

            var model = UserViewModel.FromEntity(user);

            return Result<UserViewModel>.Success(model);

        }

        public Result<int> Insert(CreateUserInputModel model)
        {
            var user = model.ToEntity();

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Result<int>.Success(user.Id);
        }

        public Result<int> InsertSkill(int userId, UserSkillsInputModel model)
        {
            if (!_dbContext.Users.Any(u => u.Id == userId))
            {
                return Result<int>.Failure(ErrorType.NotFound, "Usuário não encontrado.");
            }

            var userSkill = model.SkillIds.Select(skillId => new UserSkill(userId, skillId)).ToList();

            _dbContext.UserSkills.AddRange(userSkill);
            _dbContext.SaveChanges();

           return Result<int>.Success(userId);
        }
    }
}
