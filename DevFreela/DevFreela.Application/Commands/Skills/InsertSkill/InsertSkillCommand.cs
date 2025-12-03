using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Skills.InsertSkill
{
    public class InsertSkillCommand : IRequest<ResultViewModel<int>>
    {
        public InsertSkillCommand(string description)
        {
            Description = description;
        }
        public string Description { get; private set; }

        public Skill ToEntity()
            => new Skill(Description);
        
    }
}
