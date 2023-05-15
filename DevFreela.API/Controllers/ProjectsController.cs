﻿using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateCommand;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteCommand;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string query)
        {
            //buscar todos ou filtrar...
            var getAllProjectQuery = new GetAllProjectsQuery(query);

            var projects = await _mediator.Send(getAllProjectQuery);

            return Ok(projects);
        }
        //api/projects/423 ex
        [HttpGet("{id}")]
        public async Task <IActionResult>GetById(int id)
        {
            var query = new GetProjectByIdQuery(id); //escrevendo a query...
            var projects = await _mediator.Send(id); //usando mediator...

            if (projects == null)
            {
                return NotFound();
            }
            return Ok(projects);
        }
        [HttpPost]
        
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            if(command.Title.Length > 50)
            {
                return BadRequest();
            }
            //var id = _projectService.Create(inputModel);
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        } 
        //api/projects/2
        [HttpPut("{id}")]
        
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            if (command.Description.Length > 200)
            {
                return BadRequest();
            }
            await _mediator.Send(command);

            return NoContent();
        }

        //api/projects/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            var command = new DeleteProjectCommand(id);
            await _mediator.Send(command);

            return NoContent(); 
        }

        [HttpPost("{îd}/comments")]
        public async Task<IActionResult> PostComment(int id ,[FromBody] CreateCommentCommand command)
        {
            //onde fiz sozinho
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {
            var command = new StartProjectCommand(id);  

            await _mediator.Send(command);

            return NoContent();

        }

        [HttpPut("{id}/finish")]
        public async Task<IActionResult> Finish (int id)
        {
            var command = new FinishProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
