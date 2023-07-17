using DevFreela.Coree.Entities;
using DevFreela.Coree.Enums;
using DevFreela.Coree.InterfacesRepositorys;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //await delega a operacao de entra/sai do BD e a thread fica livre...
        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Title, request.Description, request.IdCliente, request.IdFreelancer, request.TotalCost);
            project.Comments.Add(new ProjectComment("Project was created", project.Id, project.IdCliente));

            //iniciar uma transação
            await _unitOfWork.BeginTransactionAsync();
            //cadastra um projeto
            await _unitOfWork.Projects.AddASync(project);
            await _unitOfWork.CommitAsync();
            //cadastra uma skill
            await _unitOfWork.Skills.AddSkillFromProject(project);      
            await _unitOfWork.CompleteAsync();
            //aplica as operações
            await _unitOfWork.CommitAsync();

            return project.Id;
        }
    }
}
