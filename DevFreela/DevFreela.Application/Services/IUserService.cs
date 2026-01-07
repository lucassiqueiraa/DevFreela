using DevFreela.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
    public interface IUserService
    {
        public Result<UserViewModel> GetById(int id);
        public Result<int> Insert(CreateUserInputModel model);
        public Result<int> InsertSkill(int userId, UserSkillsInputModel model);
    }
}
