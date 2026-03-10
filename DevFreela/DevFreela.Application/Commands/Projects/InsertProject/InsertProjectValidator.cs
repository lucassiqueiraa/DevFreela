using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Projects.InsertProject
{
    public class InsertProjectValidator : AbstractValidator<InsertProjectCommand>
    {
        public InsertProjectValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                    .WithMessage("O título do projeto é obrigatório.")
                .MaximumLength(50)
                    .WithMessage("O título do projeto deve conter no máximo 50 caracteres.");

            RuleFor(p => p.TotalCost)
                .GreaterThanOrEqualTo(1000)
                    .WithMessage("O custo total do projeto deve ser de pelo menos 1000");
        }
    }
}
