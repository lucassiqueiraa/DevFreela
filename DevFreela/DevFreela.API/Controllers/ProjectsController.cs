using DevFreela.Application.Models;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using DevFreela.Application.Services;
using MediatR;
using DevFreela.Application.Queries.Projects.GetAllProject;
using DevFreela.Application.Queries.Projects.GetProjectById;
using System.Threading.Tasks;
using DevFreela.Application.Commands.Projects.DeleteProject;
using DevFreela.Application.Commands.Projects.StartProject;
using DevFreela.Application.Commands.Projects.CompleteProject;
using DevFreela.Application.Commands.Projects.InsertProject;
using DevFreela.Application.Commands.Projects.InsertComment;
using DevFreela.Application.Commands.Projects.UpdateProject;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectsController(IProjectService service, IMediator mediator)
        {
           _mediator = mediator;
        }   

        //GET api/projects?search=crm
        [HttpGet]
        public async Task<IActionResult> Get(string search = "", int page = 0, int size = 3)
        {
            var query = new GetAllProjectsQuery(search, page, size);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        //GET api/projects/1234
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        //POST api/projects
        [HttpPost]  
        public async Task<IActionResult> Post(InsertProjectCommand command)
        {
            var result = await _mediator.Send(command);


            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
        }

        //PUT api/projects/1234
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProjectCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        //DELETE api/projects/1234
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        //PUT api/projects/1234/start
        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {
            var result = await _mediator.Send(new StartProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        //PUT api/projects/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            var result = await _mediator.Send(new CompleteProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        //POST api/projects/1234/comments
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, InsertCommentCommand command)
        {
            command.IdProject = id;
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent(); 
        }
    }
}
