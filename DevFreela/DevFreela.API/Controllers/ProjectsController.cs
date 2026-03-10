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
using System.Reflection.Metadata.Ecma335;

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

        [HttpGet("test-error")]
        public IActionResult GenerateError()
        {
            // Forçando um erro manual para testar o Handler
            throw new Exception("Isto é um teste do Exception Handler!");

           
        }

        //GET api/projects?search=crm
        [HttpGet]
        public async Task<IActionResult> Get(string search = "", int page = 0, int size = 3)
        {
            var query = new GetAllProjectsQuery(search, page, size);

            var result = await _mediator.Send(query);

            if(result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return result.Error switch
            {
                { Code: ErrorType.Validation } => BadRequest(result.Error.Description),
                _ => StatusCode(500, result.Error?.Description)
            };
        }

        //GET api/projects/1234
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery(id));

            if(result.IsSuccess)
            {
                return Ok(result.Data);
            }


            return result.Error switch
            {
                { Code: ErrorType.NotFound } => NotFound(result.Error.Description),
                { Code: ErrorType.Validation } => BadRequest(result.Error.Description),
                { Code: ErrorType.Conflict } => Conflict(result.Error.Description),     
                _ => StatusCode(500, result.Error?.Description)
            };
        }

        //POST api/projects
        [HttpPost]  
        public async Task<IActionResult> Post(InsertProjectCommand command)
        {
            var result = await _mediator.Send(command); 


            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
            }

            return result.Error switch
            {
                { Code: ErrorType.Validation } => BadRequest(result.Error.Description),
                { Code: ErrorType.NotFound } => NotFound(result.Error.Description),
                _ => StatusCode(500, result.Error?.Description)
            };
        }

        //PUT api/projects/1234
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProjectCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            return result.Error switch
            {
                { Code: ErrorType.NotFound } => NotFound(result.Error.Description),
                { Code: ErrorType.Validation } => BadRequest(result.Error.Description),
                _ => StatusCode(500, result.Error?.Description)
            };
        }

        //DELETE api/projects/1234
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProjectCommand(id));

            if (result.IsSuccess)
            {
                return NoContent();
            }

            return result.Error switch
            {
                { Code: ErrorType.NotFound } => NotFound(result.Error.Description),
                { Code: ErrorType.Validation } => BadRequest(result.Error.Description),
                _ => StatusCode(500, result.Error?.Description)
            };
        }

        //PUT api/projects/1234/start
        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {
            var result = await _mediator.Send(new StartProjectCommand(id));

            if (result.IsSuccess)
            {
                return NoContent();
            }

            return result.Error switch
            {
                { Code: ErrorType.NotFound } => NotFound(result.Error.Description),
                { Code: ErrorType.Validation } => BadRequest(result.Error.Description),
                _ => StatusCode(500, result.Error?.Description)
            };
        }

        //PUT api/projects/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            var result = await _mediator.Send(new CompleteProjectCommand(id));

            if (result.IsSuccess)
            {
                return NoContent();

            }

            return result.Error switch
            {
                { Code: ErrorType.NotFound } => NotFound(result.Error.Description),
                { Code: ErrorType.Validation } => BadRequest(result.Error.Description),
                _ => StatusCode(500, result.Error?.Description)
            };
        }

        //POST api/projects/1234/comments
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, InsertCommentCommand command)
        {
            command.IdProject = id;
            var result = await _mediator.Send(command);

            //TODO: retornar o comentário criado com o seu ID
            if (result.IsSuccess)
            {
                return NoContent();
            }

            return result.Error switch
            {
                { Code: ErrorType.NotFound } => NotFound(result.Error.Description),
                { Code: ErrorType.Validation } => BadRequest(result.Error.Description),
                _ => StatusCode(500, result.Error?.Description)
            };
        }
    }
}
