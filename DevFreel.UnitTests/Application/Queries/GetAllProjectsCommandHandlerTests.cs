using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Coree.Entities;
using DevFreela.Coree.InterfacesRepositorys;
using DevFreela.Coree.Models;
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

            var projects = new PaginationResult<Project>
            {   //Cria os projects fakes
                Data = new List<Project>
                {
                    new Project("Teste 1", "Descricao teste 1", 1, 2, 10000),
                    new Project("Teste 2", "Descricao teste 2", 1, 2, 20000),
                    new Project("Teste 3", "Descricao teste 3", 1, 2, 30000)

                }
            };
            

            var projectRepositoryMock = new Mock<IProjectRepository>(); //dps cria o mock do repositorio
            //usa metodo do Moq, pega o método do queryhandler, e retorna a lista fake
            projectRepositoryMock.Setup(pr => pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result).Returns(projects);

            //prox. inicializar o metodo QueryHandler
            var getAllProjectsQuery = new GetAllProjectsQuery { Query = "", Page = 1}; //passa vazio
            /*instanciar o queryHandler, controla o comportamento através do Mock retornando 3 obj "projects",
             * e passa o projectRepository para o Handler*/
            var getAllProjectsQueryHanlder = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);


            //ACT

            /* Retornar a propriedade da classe GetAllProjectQueryHandler que é o list de projectviewmodel*/
            var paginationProjectViewModelList = await getAllProjectsQueryHanlder.Handle(getAllProjectsQuery, new CancellationToken());   

            //ASSERTS

            Assert.NotNull(paginationProjectViewModelList); //ver se ta nulo
            Assert.NotEmpty(paginationProjectViewModelList.Data); //ver se não ta vazio
            Assert.Equal(projects.Data.Count, paginationProjectViewModelList.Data.Count) ; //ver o tamanho da lista se é mesmo que ta no banco fake

            //verificar se o método "projectRepository.Setup" foi chamado
            //                  Verifica o método GetAllAsync > Times é quantas vezes foi chamado
            projectRepositoryMock.Verify(pr => pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result, Times.Once);
        }
    }
}
