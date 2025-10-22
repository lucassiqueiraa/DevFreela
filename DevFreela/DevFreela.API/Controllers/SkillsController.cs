using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;

        public SkillsController(DevFreelaDbContext context)
        {
           _dbContext = context;
        }

        //GET api/skills
        [HttpGet]
        public IActionResult GetAll()
        {
            var skills = _dbContext.Skills.ToList();

            var models = skills
                .Select(SkillViewModel.FromEntity)
                .ToList();

            return Ok(models);
        }

        //POST api/skills
        [HttpPost]
        public IActionResult Post(CreateSkillInputModel model)
        {
            var skill = model.ToEntity();

            _dbContext.Skills.Add(skill);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
