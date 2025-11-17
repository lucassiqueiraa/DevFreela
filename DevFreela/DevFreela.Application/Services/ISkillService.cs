using DevFreela.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
    public interface ISkillService
    {
        ResultViewModel<List<SkillViewModel>> GetAllSkill();
        ResultViewModel<SkillViewModel> GetById(int id);
        ResultViewModel<int> Insert(CreateSkillInputModel model);
    }
}
