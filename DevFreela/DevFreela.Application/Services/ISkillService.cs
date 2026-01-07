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
        Result<List<SkillViewModel>> GetAllSkill();
        Result<SkillViewModel> GetById(int id);
        Result<int> Insert(CreateSkillInputModel model);
    }
}
