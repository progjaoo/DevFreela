//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using DevFreela.Application.Commands.DeleteCommand;
//using DevFreela.Coree.Entities;
//using DevFreela.Coree.Repositories;
//using Moq;

//namespace DevFreel.UnitTests.Application.Commands
//{
//    public class DeleteProjectCommandHandlerTests
//    {
//        [Fact]

//        public async Task ProjectNotExists_Executed_DoesNothing()
//        {
//            //ARRANGE
//            // Arrange
//            var projectId = 1;
//            var projectRepositoryMock = new Mock<IProjectRepository>();
//            projectRepositoryMock
//                .Setup(r => r.GetByIdAsync(projectId))
//                .ReturnsAsync((Project)null);

//            var command = new DeleteProjectCommand(projectId);
//            var commandHandler = new DeleteProjecCommandHandler(projectRepositoryMock.Object);

//            // Act
//            await commandHandler.Handle(command, CancellationToken.None);

//            // Assert
//            projectRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<Project>()), Times.Never);

//        }
//    }
//}
