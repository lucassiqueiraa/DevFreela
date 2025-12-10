using DevFreela.Application.Commands.Skills.InsertSkill;
using DevFreela.Application.Commands.Users.InsertUser;
using DevFreela.Application.Commands.Users.InsertUserSkill;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.Users.GetUserById;
using DevFreela.Application.Services;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        private readonly IMediator _mediator;
        public UsersController (IUserService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if(!result.IsSuccess)
            {
                return NotFound(result.Message);
            }

            return Ok(result);
        }

        //POST api/users
        [HttpPost]
        public async Task<IActionResult> PostAsync(InsertUserCommand command)
        {
            var result = await _mediator.Send(command);

            if(!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Data }, result);
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkill(int id, UserSkillsInputModel model)
        {
            
            var result = await _mediator.Send(new InsertUserSkillCommand(id, model.SkillIds));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult PutProfilePicture(int id, IFormFile file) //para receber arquivos na api
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            //Processar a imagem, ex: salvar no banco de dados, salvar local, salvar blob storage ou s3.

            return Ok(description);
        }
    }


}
