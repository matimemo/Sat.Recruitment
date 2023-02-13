using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Sat.Recruitment.DAL;
using Sat.Recruitment.Model;
using Sat.Recruitment.Services;
using Xunit;

namespace Sat.Recruitment.Servicies.Test
{
    public class UserServiceTest
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private UserService _userService;
        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task When_We_Call_UserService_AddIfNotExist_And_The_User_Dont_Exist_Should_Return_True()
        {
            //Arrange
            var user = Mock.Of<User>();
            _userRepositoryMock.Setup(userRepository => userRepository.Exists(It.IsAny<User>())).ReturnsAsync(false);
            _userRepositoryMock.Setup(userRepository => userRepository.Add(It.IsAny<User>()));

            //Act
            var result = await _userService.AddIfNotExist(user);

            //Assert
            _userRepositoryMock.Verify(urm => urm.Add(user), Times.Once);
            _userRepositoryMock.Verify(urm => urm.Exists(user), Times.Once);
            result.Should().BeTrue();
        }

        [Fact]
        public async Task When_We_Call_UserService_AddIfNotExist_And_The_User_Exist_Should_Return_False()
        {
            //Arrange
            var user = Mock.Of<User>();
            _userRepositoryMock.Setup(userRepository => userRepository.Exists(It.IsAny<User>())).ReturnsAsync(true);
            _userRepositoryMock.Setup(userRepository => userRepository.Add(It.IsAny<User>()));

            //Act
            var result = await _userService.AddIfNotExist(user);

            //Assert
            _userRepositoryMock.Verify(urm => urm.Add(user),Times.Never);
            _userRepositoryMock.Verify(urm => urm.Exists(user),Times.Once);
            result.Should().BeFalse();
        }
    }
}
