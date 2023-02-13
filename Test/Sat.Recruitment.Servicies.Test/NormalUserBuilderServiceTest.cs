using Moq;
using Sat.Recruitment.Model.Dtos;
using Sat.Recruitment.Model;
using Sat.Recruitment.Services;
using Sat.Recruitment.Services.Interfaces;
using FluentAssertions;
using Xunit;

namespace Sat.Recruitment.Servicies.Test
{
    public class NormalUserBuilderServiceTest
    {
        private Mock<IEmailService> _mockEmailService;
        private NormalUserBuilder _normalUserBuilder;
        public NormalUserBuilderServiceTest()
        {
            _mockEmailService = new Mock<IEmailService>();
            _normalUserBuilder = new NormalUserBuilder(_mockEmailService.Object);
        }

        [Fact]
        public void When_NormalUserBuilder_Calls_Build_It_Return_A_User_With_The_Corresponding_Properties()
        {
            //arrange
            var userDto = Mock.Of<UserDto>(u => u.Name == "Matias"
                                             && u.Email == "matiasmemolli@gmail.com"
                                             && u.Address == "9 968"
                                             && u.Phone == "221 5920970"
                                             && u.UserType == UserType.Normal.ToString()
                                             && u.Money == "1000");
            _mockEmailService
                .Setup(n => n.Normalize(userDto.Email))
                .Returns(userDto.Email);

            //act
            var user = _normalUserBuilder.Build(userDto);

            //assert
            user.Should().BeOfType<User>();
            user.Name.Should().BeEquivalentTo(userDto.Name);
            user.Email.Should().BeEquivalentTo(userDto.Email);
            user.Address.Should().BeEquivalentTo(userDto.Address);
            user.Phone.Should().BeEquivalentTo(userDto.Phone);
            user.UserType.Should().HaveSameNameAs(UserType.Normal);
        }

        [Theory]
        [InlineData("5", 5)]
        [InlineData("10", 10)]
        public void When_NormalUserBuilder_Calls_Build_With_Less_Than_10_Must_No_Apply_Gif_Money(string moneyInput,decimal moneyExpected)
        {
            //arrange
            var userDto = Mock.Of<UserDto>(u => u.Name == "Matias"
                                                && u.Email == "matiasmemolli@gmail.com"
                                                && u.Address == "9 968"
                                                && u.Phone == "221 5920970"
                                                && u.UserType == UserType.Normal.ToString()
                                                && u.Money == moneyInput);
            _mockEmailService
                .Setup(n => n.Normalize(userDto.Email))
                .Returns(userDto.Email);

            //act
            var user = _normalUserBuilder.Build(userDto);

            //assert
            user.Money.Should().Be(moneyExpected);
        }

        [Theory]
        [InlineData(20)]
        [InlineData(90)]
        public void When_NormalUserBuilder_Calls_Build_With_Less_Than_100_and_More_Than_10_Must_Apply_08_Gif_Money(decimal money)
        {
            //arrange
            var moneyExpected = money + (money * 0.8M);
            var userDto = Mock.Of<UserDto>(u => u.Name == "Matias"
                                                && u.Email == "matiasmemolli@gmail.com"
                                                && u.Address == "9 968"
                                                && u.Phone == "221 5920970"
                                                && u.UserType == UserType.Normal.ToString()
                                                && u.Money == money.ToString());
            _mockEmailService
                .Setup(n => n.Normalize(userDto.Email))
                .Returns(userDto.Email);

            //act
            var user = _normalUserBuilder.Build(userDto);

            //assert
            user.Money.Should().Be(moneyExpected);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(101)]
        public void When_NormalUserBuilder_Calls_Build_With_More_Than_100_Must_Apply_12_Gif_Money(decimal money)
        {
            //arrange
            var moneyExpected = money + (money * 0.12M);
            var userDto = Mock.Of<UserDto>(u => u.Name == "Matias"
                                                && u.Email == "matiasmemolli@gmail.com"
                                                && u.Address == "9 968"
                                                && u.Phone == "221 5920970"
                                                && u.UserType == UserType.Normal.ToString()
                                                && u.Money == money.ToString());
            _mockEmailService
                .Setup(n => n.Normalize(userDto.Email))
                .Returns(userDto.Email);

            //act
            var user = _normalUserBuilder.Build(userDto);

            //assert
            user.Money.Should().Be(moneyExpected);
        }
    }
}
