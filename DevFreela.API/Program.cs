using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateCommand;
using DevFreela.Application.Commands.DeleteCommand;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Coree.Repositories;
using DevFreela.Infrastructure.Persistence.Repositories;
using FluentValidation.AspNetCore;
using DevFreela.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());


//adicionando os tipos do mediator para injeção de dependencia!!
builder.Services.AddMediatR(typeof(CreateProjectCommand));
builder.Services.AddMediatR(typeof(CreateCommentCommand));
builder.Services.AddMediatR(typeof(DeleteProjectCommand));
builder.Services.AddMediatR(typeof(UpdateProjectCommand));
builder.Services.AddMediatR(typeof(FinishProjectCommand));
builder.Services.AddMediatR(typeof(StartProjectCommand));
builder.Services.AddMediatR(typeof(CreateUserCommand));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("DevFreelaCs");
builder.Services.AddDbContext<DevFreelaDbContext>(p => p.UseSqlServer(connection));

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
