using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;

        public UsersController (DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _dbContext.Users
                .Include(u => u.Skills)
                    .ThenInclude(us => us.Skill)
                .SingleOrDefault(u => u.Id == id);

            if (user == null) return NotFound();

            var model = UserViewModel.FromEntity(user);

            return Ok(model);
        }

        //POST api/users
        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var user = model.ToEntity();

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Created();
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkill(int id, UserSkillsInputModel model)
        {
            var userSkill = model.SkillIds.Select(skillId => new UserSkill(id, skillId)).ToList();

            _dbContext.UserSkills.AddRange(userSkill);
            _dbContext.SaveChanges();

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
