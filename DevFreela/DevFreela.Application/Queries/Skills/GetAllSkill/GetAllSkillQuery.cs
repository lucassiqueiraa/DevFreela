using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.Skills.GetAllSkill
{
    public class GetAllSkillQuery : IRequest<ResultViewModel<List<SkillViewModel>>>
    {
    }
}
