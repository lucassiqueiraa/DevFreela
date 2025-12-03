using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.Skills.GetSkillById
{
    public class GetSkillByIdQuery : IRequest<ResultViewModel<SkillViewModel>>
    {
        public GetSkillByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }
}
