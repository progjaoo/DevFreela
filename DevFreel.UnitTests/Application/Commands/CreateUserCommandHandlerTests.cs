//using DevFreel.UnitTests.Infrastructure.Persistence;
//using DevFreela.Application.Commands.CreateUser;
//using DevFreela.Coree.Entities;
//using DevFreela.Coree.Services;
//using DevFreela.Infrastructure.Persistence;
//using Microsoft.EntityFrameworkCore;
//using Moq;

//namespace DevFreel.UnitTests.Application.Commands
//{
//    public class CreateUserCommandHandlerTests
//    {
//        [Fact]
//        public async Task InputDataIsOk_Executed_ReturnUserId()
//        {
//            // Arrange

//            var options = new DbContextOptionsBuilder<DevFreelaDbContext>()
//                            .UseInMemoryDatabase(databaseName: "TestDatabase")
//                            .Options; //criei no Infraestructure.Persistence

//            var dbContext = new Mock<TestDbContext>(options);
//            var authService = new Mock<IAuthService>();

//            var createUserCommand = new CreateUserCommand
//            {
//                FullName = "John Doe",
//                Email = "johndoe@example.com",
//                Password = "password",
//                BirthDate = new DateTime(1990, 1, 1),
//                Role = "User"
//            };

//            var createUserCommandHandler = new CreateUserCommandHandler(dbContext.Object, authService.Object);

//            // Act
//            var userId = await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());

//            // Assert
//            Assert.True(userId >= 0);
//            dbContext.Verify(db => db.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
//            dbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
//        }
//    }
//}
