using DevFreela.Application.Models;
using DevFreela.Application.Services;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController (IUserService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);

            if(!result.IsSuccess)
            {
                return NotFound(result.Message);
            }

            return Ok(result);
        }

        //POST api/users
        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var result = _service.Insert(model);

            if(!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkill(int id, UserSkillsInputModel model)
        {
            var result = _service.InsertSkill(id, model);

            if(!result.IsSuccess)
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
