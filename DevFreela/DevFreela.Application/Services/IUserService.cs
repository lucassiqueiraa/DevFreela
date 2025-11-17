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
        public ResultViewModel<UserViewModel> GetById(int id);
        public ResultViewModel<int> Insert(CreateUserInputModel model);
        public ResultViewModel<int> InsertSkill(int userId, UserSkillsInputModel model);
    }
}
