using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Skills.InsertSkill
{
    public class InsertSkillValidator : AbstractValidator<InsertSkillCommand>
    {
        public InsertSkillValidator()
        {
            RuleFor(s => s.Description)
                .NotEmpty()
                    .WithMessage("A descrição é obrigatória.")
                .MaximumLength(50)
                    .WithMessage("A descrição deve conter no máximo 50 caracteres.");
        }
    }
}
