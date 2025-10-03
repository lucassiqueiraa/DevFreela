using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using DevFreela.API.Services;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly FreelanceTotalCostConfig _config;  
        private readonly IConfigService _configService;
        public ProjectsController(
            IOptions<FreelanceTotalCostConfig> options,
            IConfigService configService)
        {
            _config = options.Value;
            _configService = configService;
        }

        //GET api/projects?search=crm
        [HttpGet]
        public IActionResult Get(string search = "")
        {
            return Ok(_configService.GetValue());
        }

        //GET api/projects/1234
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            throw new Exception();

            return Ok();
        }

        //POST api/projects
        [HttpPost]  
        public IActionResult Post(CreateProjectInputModel model)
        {
            if(model.TotalCost < _config.Minimum || model.TotalCost > _config.Maximum)
            {
                return BadRequest($"The project total cost must be between {_config.Minimum} and {_config.Maximum}");
            }
            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        //PUT api/projects/1234
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            return NoContent();
        }

        //DELETE api/projects/1234
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }

        //PUT api/projects/1234/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        //PUT api/projects/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            return NoContent();
        }

        //POST api/projects/1234/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            return Ok();
        }
    }
}
