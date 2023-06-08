using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Coree.Entities;
using DevFreela.Coree.Repositories;
using Moq;

namespace DevFreel.UnitTests.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
                    //GIVEN             WHEN     THENN
        public async Task ThreeProjectsExists_Executed_ReturnThreeProjectViewModels()
        {
            //Arrange 
            
            var projects = new List<Project>() //Cria os projects fakes
            {
                new Project ("Teste 1", "Descricao teste 1", 1, 2, 10000),
                new Project ("Teste 2", "Descricao teste 2", 1, 2, 20000),
                new Project ("Teste 3", "Descricao teste 3", 1, 2, 30000)
            };

            var projectRepositoryMock = new Mock<IProjectRepository>(); //dps cria o mock do repositorio
            //usa metodo do Moq, pega o método do queryhandler, e retorna a lista fake
            projectRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(projects);

            //prox. inicializar o metodo QueryHandler
            var getAllProjectsQuery = new GetAllProjectsQuery(""); //passa vazio
            /*instanciar o queryHandler, controla o comportamento através do Mock retornando 3 obj "projects",
             * e passa o projectRepository para o Handler*/
            var getAllProjectsQueryHanlder = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);


            //ACT

            /* Retornar a propriedade da classe GetAllProjectQueryHandler que é o list de projectviewmodel*/
            var projectViewModelList = await getAllProjectsQueryHanlder.Handle(getAllProjectsQuery, new CancellationToken());   

            //Asserts
            Assert.NotNull(projectViewModelList); //ver se ta nulo
            Assert.NotEmpty(projectViewModelList); //ver se não ta vazio
            Assert.Equal(projects.Count, projectViewModelList.Count); //ver o tamanho da lista se é mesmo que ta no banco fake

            //verificar se o método "projectRepository.Setup" foi chamado
            //                  Verifica o método GetAllAsync > Times é quantas vezes foi chamado
            projectRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once);
        }
    }
}
