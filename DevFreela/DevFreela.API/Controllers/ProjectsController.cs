using DevFreela.Application.Models;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;
        public ProjectsController(DevFreelaDbContext context)
        {
           _dbContext = context;
        }

        //GET api/projects?search=crm
        [HttpGet]
        public IActionResult Get(string search = "", int page = 0, int size = 3)
        {
            var projects = _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                .Skip(page * size)
                .Take(size)
                .ToList();

            var model = projects
                .Select(ProjectItemViewModel.FromEntity)
                .ToList();

            return Ok(model);
        }

        //GET api/projects/1234
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id );

            if (project is null)
            {
                return NotFound();
            }

            var model = ProjectViewModel.FromEntity(project);

            return Ok(model);
        }

        //POST api/projects
        [HttpPost]  
        public IActionResult Post(CreateProjectInputModel model)
        {
            var project = model.ToEntity();

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        //PUT api/projects/1234
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id); //project trackeado
           
            if(project is null)
            {
                return NotFound();
            }

            project.Update(model.Title, model.Description, model.TotalCost); //talvez desnecessário pq ja está trackeado

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //DELETE api/projects/1234
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id); //project trackeado

            if (project is null)
            {
                return NotFound();
            }

            project.SetAsDeleted();
            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //PUT api/projects/1234/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id); //project trackeado

            if (project is null)
            {
                return NotFound();
            }

            project.Start();
            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //PUT api/projects/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return NotFound();
            }

            project.Complete();
            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //POST api/projects/1234/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return NotFound();
            }

            var comment = model.ToEntity();

            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
