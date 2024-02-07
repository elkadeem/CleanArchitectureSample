using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Core.UnitTest
{
    public class UsersServiceUnitTests
    {
        [Fact]
        public async Task Add_User_Will_Throw_Exception_If_User_Is_Null()
        {            
            // Arrange
            // Generate users services with mock repository and logger
            var usersRepository = new Mock<IUsersRepository>();
            var logger = new Mock<ILogger<UsersService>>();
            var usersService = new UsersService(usersRepository.Object, logger.Object);            
            // Act
            async Task act() => await usersService.AddAsync(null);

            // Assert
            await Assert.ThrowsAnyAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task Add_User_Will_Throw_Exception_If_User_Email_Used()
        {
            // Arrange
            // Generate users services with mock repository and logger
            var usersRepository = new Mock<IUsersRepository>();
            var logger = new Mock<ILogger<UsersService>>();
            var usersService = new UsersService(usersRepository.Object, logger.Object);
            var user = new User(0, "testname"
                , "test user", "conflit@company.com");
            usersRepository.Setup(x => x.IsUserEmailExistAsync(0, user.Email))
                .ReturnsAsync(true);
            // Act
            async Task act() => await usersService.AddAsync(user);

            // Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
        }
    }

}