using FluentValidation;
using Sat.Recruitment.Model.Dtos;
using Sat.Recruitment.Services.Interfaces;
using System;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Validations;
using Sat.Recruitment.Model;
using Xunit;
using Sat.Recruitment.Services;

namespace Sat.Recruitment.Api.Test
{
    public class UserControllerTest
    {
        private Mock<IUserFactory> _userFactory;
        private Mock<IUserService> _userService;
        private Mock<IEmailService> _emailService;
        private UserDtoValidator _validator;
        private Mock<IValidator<UserDto>> _validatorToInject;
        private UsersController _usersController;

        public UserControllerTest()
        {
            _userFactory = new Mock<IUserFactory>();
            _userService = new Mock<IUserService>();
            _emailService = new Mock<IEmailService>();
            _validatorToInject = new Mock<IValidator<UserDto>>();
            _validator = new UserDtoValidator();
            _usersController = new UsersController(_userFactory.Object, _userService.Object, _validatorToInject.Object);
        }

        [Fact]
        public async Task When_Model_Is_Not_Correct_Should_Return_Error()
        {
            //Act
            var userDto = new UserDto();
            var resultValidation = _validator.TestValidate(userDto);
            _validatorToInject.Setup(v => v.Validate(It.IsAny<UserDto>())).Returns(resultValidation);

            //Arrange
            var resultController = await _usersController.CreateUser(userDto);

            //Assert
            _userFactory.Verify(uf => uf.Resolve(It.IsAny<UserType>()), Times.Never);
            _userService.Verify(us => us.AddIfNotExist(It.IsAny<User>()), Times.Never);
            Assert.True(resultController is BadRequestObjectResult);
        }

        [Fact]
        public async Task When_Model_Is_Correct_But_User_Exist_Should_Return_Error()
        {
            //Act
            var userDto = new UserDto()
            {
                Email = "matiasmemolli@gmail.com",
                Address = "9 968",
                Money = "1000",
                Name = "Matias",
                UserType = "Normal",
                Phone = "2215920970"
            };
            var resultValidation = _validator.TestValidate(userDto);
            var builder = new NormalUserBuilder(_emailService.Object);

            _emailService.Setup(es => es.Normalize(It.IsAny<string>())).Returns(userDto.Email);
            _validatorToInject.Setup(v => v.Validate(It.IsAny<UserDto>())).Returns(resultValidation);
            _userFactory.Setup(uf => uf.Resolve(It.Is<UserType>(x => x == UserType.Normal))).Returns(builder);
            _userService.Setup(us => us.AddIfNotExist(It.IsAny<User>())).ReturnsAsync(false);

            //Arrange
            var resultController = await _usersController.CreateUser(userDto);

            //Assert
            _userFactory.Verify(uf => uf.Resolve(It.IsAny<UserType>()), Times.Once);
            _userService.Verify(us => us.AddIfNotExist(It.IsAny<User>()), Times.Once);
            Assert.True(resultController is ConflictObjectResult);
        }
        [Fact]
        public async Task When_Model_Is_Correct_But_UserSerice_Throws_Exception_Should_Return_Error()
        {
            //Act
            var userDto = new UserDto()
            {
                Email = "matiasmemolli@gmail.com",
                Address = "9 968",
                Money = "1000",
                Name = "Matias",
                UserType = "Normal",
                Phone = "2215920970"
            };
            var resultValidation = _validator.TestValidate(userDto);
            var builder = new NormalUserBuilder(_emailService.Object);

            _emailService.Setup(es => es.Normalize(It.IsAny<string>())).Returns(userDto.Email);
            _validatorToInject.Setup(v => v.Validate(It.IsAny<UserDto>())).Returns(resultValidation);
            _userFactory.Setup(uf => uf.Resolve(It.Is<UserType>(x => x == UserType.Normal))).Returns(builder);
            _userService.Setup(us => us.AddIfNotExist(It.IsAny<User>())).Throws(new Exception("Test"));

            //Arrange
            var resultController = await _usersController.CreateUser(userDto);

            //Assert
            _userFactory.Verify(uf => uf.Resolve(It.IsAny<UserType>()), Times.Once);
            _userService.Verify(us => us.AddIfNotExist(It.IsAny<User>()), Times.Once);
            Assert.True(resultController is StatusCodeResult);
            Assert.Equal(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ((StatusCodeResult)resultController).StatusCode);
        }
        [Fact]
        public async Task When_All_Is_Correct_Should_Not_Return_Error()
        {
            //Act
            var userDto = new UserDto()
            {
                Email = "matiasmemolli@gmail.com",
                Address = "9 968",
                Money = "1000",
                Name = "Matias",
                UserType = "Normal",
                Phone = "2215920970"
            };
            var resultValidation = _validator.TestValidate(userDto);
            var builder = new NormalUserBuilder(_emailService.Object);

            _emailService.Setup(es => es.Normalize(It.IsAny<string>())).Returns(userDto.Email);
            _validatorToInject.Setup(v => v.Validate(It.IsAny<UserDto>())).Returns(resultValidation);
            _userFactory.Setup(uf => uf.Resolve(It.Is<UserType>(x => x == UserType.Normal))).Returns(builder);
            _userService.Setup(us => us.AddIfNotExist(It.IsAny<User>())).ReturnsAsync(true);

            //Arrange
            var resultController = await _usersController.CreateUser(userDto);

            //Assert
            _userFactory.Verify(uf => uf.Resolve(It.IsAny<UserType>()), Times.Once);
            _userService.Verify(us => us.AddIfNotExist(It.IsAny<User>()), Times.Once);
            Assert.True(resultController is CreatedAtActionResult);
            Assert.Equal(Microsoft.AspNetCore.Http.StatusCodes.Status201Created, ((CreatedAtActionResult)resultController).StatusCode);
        }
    }
}
