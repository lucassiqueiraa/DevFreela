using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Users.InsertUserSkill
{
    public class InsertUserSkillCommand : IRequest<ResultViewModel<int>>
    {
        public InsertUserSkillCommand(int userId, int[] skillIds)
        {
            UserId = userId;
            SkillIds = skillIds;

        }

        public int UserId { get; private set; }
        public int[] SkillIds { get; private set; }

    }
  }
