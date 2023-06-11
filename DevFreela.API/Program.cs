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
using DevFreela.Coree.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Application.Commands.LoginUser;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)))
//    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());


//adicionando os tipos do mediator para injeção de dependencia!!
builder.Services.AddMediatR(typeof(CreateProjectCommand));
builder.Services.AddMediatR(typeof(CreateCommentCommand));
builder.Services.AddMediatR(typeof(DeleteProjectCommand));
builder.Services.AddMediatR(typeof(UpdateProjectCommand));
builder.Services.AddMediatR(typeof(FinishProjectCommand));
builder.Services.AddMediatR(typeof(StartProjectCommand));
builder.Services.AddMediatR(typeof(CreateUserCommand));
builder.Services.AddMediatR(typeof(LoginUserCommand));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
var connection = builder.Configuration.GetConnectionString("DevFreelaCs");
builder.Services.AddDbContext<DevFreelaDbContext>(p => p.UseSqlServer(connection));

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevFreela.API v1"));
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
