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

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        //GET api/skills/234
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetSkillByIdQuery(id));

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }

            return Ok(result);
        }

        //POST api/skills
        [HttpPost]
        public async Task<IActionResult> Post(InsertSkillCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new {id = result.Data}, result);
        }
    }
}
