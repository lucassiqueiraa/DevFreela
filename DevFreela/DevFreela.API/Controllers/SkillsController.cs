using DevFreela.Application.Commands.Skills.InsertSkill;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.Skills.GetAllSkill;
using DevFreela.Application.Queries.Skills.GetSkillById;
using DevFreela.Application.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillsController(ISkillService service, IMediator mediator)
        {
           _mediator = mediator;
        }

        //GET api/skills
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllSkillQuery());

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return result.Error switch
            {
                { Code: ErrorType.Validation } => BadRequest(result.Error.Description),
                _ => StatusCode(500, result.Error?.Description)
            };
        }

        //GET api/skills/234
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetSkillByIdQuery(id));

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return result.Error switch
            {
                { Code: ErrorType.NotFound } => NotFound(result.Error.Description),
                _ => StatusCode(500, result.Error?.Description)
            };
           
        }

        //POST api/skills
        [HttpPost]
        public async Task<IActionResult> Post(InsertSkillCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
            }

            return result.Error switch
            {
                { Code : ErrorType.Conflict } => Conflict(result.Error.Description),
                { Code: ErrorType.Validation } => BadRequest(result.Error.Description),
                _ => StatusCode(500, result.Error?.Description)
            };
        }
    }
}
