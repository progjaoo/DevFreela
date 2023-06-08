using DevFreela.Application.Commands.CreateProject;
using DevFreela.Coree.Entities;
using DevFreela.Coree.Repositories;
using Moq;

namespace DevFreel.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
                        //given dado q WHEN-qnd   THEN retorna                
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            //ARRANGE
            var projectRepository = new Mock<IProjectRepository>();

            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Titulo teste",
                Description = "Descricao test",
                TotalCost = 20000,
                IdCliente = 2,
                IdFreelancer = 3
            };
            //INIT COMMAND HANDLER
            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepository.Object);

            //ACT

            /*retorna um ID de projeto*/
            var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

            //ASSERTS
            Assert.True(id >= 0);
            /*Checar se o metodo AddAsync foi chamado*/
            projectRepository.Verify(pr => pr.AddASync(It.IsAny<Project>()), Times.Once);
        }
    }
}
