using FluentValidation;
using System.Linq;
using System.Collections.Generic;

namespace DevFreela.Application.Commands.Users.InsertUserSkill
{
    public class InsertUserSkillValidator : AbstractValidator<InsertUserSkillCommand>
    {
        public InsertUserSkillValidator()
        {
            RuleFor(us => us.UserId)
                .GreaterThan(0)
                    .WithMessage("O Id do usuário deve ser maior que zero.");

            RuleFor(us => us.SkillIds)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("A lista de Ids de habilidades não pode estar vazia.")
                .Must(ids => ids.All(id => id > 0))
                    .WithMessage("Todos os Ids de habilidades devem ser maiores que zero.")
                .Must(ids => new HashSet<int>(ids).Count == ids.Length)
                    .WithMessage("A lista de Ids de habilidades não pode conter valores duplicados.");
        }
    }
}
