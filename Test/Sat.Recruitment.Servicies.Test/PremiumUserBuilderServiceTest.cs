using FluentAssertions;
using Moq;
using Sat.Recruitment.Model;
using Sat.Recruitment.Model.Dtos;
using Sat.Recruitment.Services;
using Sat.Recruitment.Services.Interfaces;
using Xunit;

namespace Sat.Recruitment.Servicies.Test
{
    public class PremiumUserBuilderServiceTest
    {
        private Mock<IEmailService> _mockEmailService;
        private PremiumUserBuilder _premiumUserBuilder;
        public PremiumUserBuilderServiceTest()
        {
            _mockEmailService = new Mock<IEmailService>();
            _premiumUserBuilder = new PremiumUserBuilder(_mockEmailService.Object);
        }

        [Fact]
        public void When_PremiumUserBuilder_Calls_Build_It_Return_A_User_With_The_Corresponding_Properties()
        {
            //arrange
            var userDto = Mock.Of<UserDto>(u => u.Name == "Matias"
                                                && u.Email == "matiasmemolli@gmail.com"
                                                && u.Address == "9 968"
                                                && u.Phone == "221 5920970"
                                                && u.UserType == UserType.Premium.ToString()
                                                && u.Money == "1000");
            _mockEmailService
                .Setup(n => n.Normalize(userDto.Email))
                .Returns(userDto.Email);

            //act
            var user = _premiumUserBuilder.Build(userDto);

            //assert
            user.Should().BeOfType<User>();
            user.Name.Should().BeEquivalentTo(userDto.Name);
            user.Email.Should().BeEquivalentTo(userDto.Email);
            user.Address.Should().BeEquivalentTo(userDto.Address);
            user.Phone.Should().BeEquivalentTo(userDto.Phone);
            user.UserType.Should().HaveSameNameAs(UserType.Premium);
        }

        [Theory]
        [InlineData("5", 5)]
        [InlineData("99", 99)]
        public void When_PremiumUserBuilder_Calls_Build_With_Less_Than_100_Must_No_Apply_Gif_Money(string moneyInput, decimal moneyExpected)
        {
            //arrange
            var userDto = Mock.Of<UserDto>(u => u.Name == "Matias"
                                                && u.Email == "matiasmemolli@gmail.com"
                                                && u.Address == "9 968"
                                                && u.Phone == "221 5920970"
                                                && u.UserType == UserType.Premium.ToString()
                                                && u.Money == moneyInput);
            _mockEmailService
                .Setup(n => n.Normalize(userDto.Email))
                .Returns(userDto.Email);

            //act
            var user = _premiumUserBuilder.Build(userDto);

            //assert
            user.Money.Should().Be(moneyExpected);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(101)]
        public void When_PremiumUserBuilder_Calls_Build_With_More_Than_100_Must_Apply_2_Gif_Money(decimal money)
        {
            //arrange
            var moneyExpected = money + (money * 2M);
            var userDto = Mock.Of<UserDto>(u => u.Name == "Matias"
                                                && u.Email == "matiasmemolli@gmail.com"
                                                && u.Address == "9 968"
                                                && u.Phone == "221 5920970"
                                                && u.UserType == UserType.Premium.ToString()
                                                && u.Money == money.ToString());
            _mockEmailService
                .Setup(n => n.Normalize(userDto.Email))
                .Returns(userDto.Email);

            //act
            var user = _premiumUserBuilder.Build(userDto);

            //assert
            user.Money.Should().Be(moneyExpected);
        }
    }
}