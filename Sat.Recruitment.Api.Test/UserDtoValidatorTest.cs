using FluentAssertions;
using FluentValidation.TestHelper;
using Sat.Recruitment.Api.Validations;
using Sat.Recruitment.Model.Dtos;
using Xunit;

namespace Sat.Recruitment.Api.Test
{
    public class UserDtoValidatorTest
    {
        private UserDtoValidator _validator;
        public UserDtoValidatorTest()
        {
            _validator = new UserDtoValidator();
        }

        [Fact]
        public void When_Input_Is_Null_Should_Fail()
        {
            //arrange
            var userDto = new UserDto();
            //Act
            var validationResult = _validator.TestValidate(userDto);

            //Asert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ShouldHaveValidationErrorFor(user => user.Email);
            validationResult.ShouldHaveValidationErrorFor(user => user.Address);
            validationResult.ShouldHaveValidationErrorFor(user => user.Name);
            validationResult.ShouldHaveValidationErrorFor(user => user.Phone);
            validationResult.ShouldHaveValidationErrorFor(user => user.Money);
            validationResult.ShouldHaveValidationErrorFor(user => user.UserType);
        }

        [Fact]
        public void When_Input_Is_Empty_Should_Fail()
        {
            //arrange
            var userDto = new UserDto()
            {
                Email = "",
                Address = "",
                Money = "",
                Name = "",
                UserType = "",
                Phone = ""
            };
            //Act
            var validationResult = _validator.TestValidate(userDto);

            //Asert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ShouldHaveValidationErrorFor(user => user.Email);
            validationResult.ShouldHaveValidationErrorFor(user => user.Address);
            validationResult.ShouldHaveValidationErrorFor(user => user.Name);
            validationResult.ShouldHaveValidationErrorFor(user => user.Phone);
            validationResult.ShouldHaveValidationErrorFor(user => user.Money);
            validationResult.ShouldHaveValidationErrorFor(user => user.UserType);
        }

        [Fact]
        public void When_Input_Has_Correct_Values_Should_Not_Fail()
        {
            //arrange
            var userDto = new UserDto()
            {
                Email = "matiasmemolli@gmail.com",
                Address = "9 968",
                Money = "1000",
                Name = "Matias",
                UserType = "Normal",
                Phone = "2215920970"
            };
            //Act
            var validationResult = _validator.TestValidate(userDto);

            //Asert
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void When_Input_Has_Incorrect_Money_Value_Should_Fail()
        {
            //arrange
            var userDto = new UserDto()
            {
                Email = "matiasmemolli@gmail.com",
                Address = "9 968",
                Money = "Incorrect",
                Name = "Matias",
                UserType = "Normal",
                Phone = "2215920970"
            };
            //Act
            var validationResult = _validator.TestValidate(userDto);

            //Asert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ShouldHaveValidationErrorFor(user => user.Money);
        }

        [Fact]
        public void When_Input_Has_Incorrect_Email_Value_Should_Fail()
        {
            //arrange
            var userDto = new UserDto()
            {
                Email = "matiasmemolligmail.com",
                Address = "9 968",
                Money = "1324",
                Name = "Matias",
                UserType = "Normal",
                Phone = "2215920970"
            };
            //Act
            var validationResult = _validator.TestValidate(userDto);

            //Asert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ShouldHaveValidationErrorFor(user => user.Email);
        }

        [Fact]
        public void When_Input_Has_Incorrect_UserType_Value_Should_Fail()
        {
            //arrange
            var userDto = new UserDto()
            {
                Email = "matiasmemolligmail.com",
                Address = "9 968",
                Money = "1324",
                Name = "Matias",
                UserType = "Master",
                Phone = "2215920970"
            };
            //Act
            var validationResult = _validator.TestValidate(userDto);

            //Asert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ShouldHaveValidationErrorFor(user => user.UserType);
        }
    }
}
