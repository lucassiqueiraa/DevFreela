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

        private readonly IMediator _mediator;
        public UsersController (IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if(result.IsSuccess)
            {
                return Ok(result);
            }

            return result.Error switch
            {
                { Code: ErrorType.NotFound } => NotFound(result.Error.Description),
                _ => StatusCode(500, result.Error?.Description)
            };
        }

        //POST api/users
        [HttpPost]
        public async Task<IActionResult> PostAsync(InsertUserCommand command)
        {
            var result = await _mediator.Send(command);

            if(!result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Data }, result);
            }

            return result.Error switch
            {
                { Code: ErrorType.Validation } => BadRequest(result.Error.Description),
                _ => StatusCode(500, result.Error?.Description)
            };
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkill(int id, UserSkillsInputModel model)
        {
            var result = await _mediator.Send(new InsertUserSkillCommand(id, model.SkillIds));

            if (result.IsSuccess)
            {
                return NoContent();
            }

            return result.Error switch
            {
                { Code: ErrorType.Validation } => BadRequest(result.Error.Description),
                { Code: ErrorType.NotFound } => NotFound(result.Error.Description),
                _ => StatusCode(500, result.Error?.Description)
            };
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
