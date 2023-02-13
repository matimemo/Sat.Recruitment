using FluentAssertions;
using Moq;
using Sat.Recruitment.Model;
using Sat.Recruitment.Services;
using Sat.Recruitment.Services.Interfaces;
using Sat.Recruitment.Services.Interfaces.BuilderInterfaces;
using Xunit;

namespace Sat.Recruitment.Servicies.Test
{
    public class UserFactoryTest
    {
        private UserFactory _userFactory;
        private Mock<IEmailService> _emailServiceMock;
        private INormalUserBuilder _normalUserBuilder;
        private ISuperUserBuilder _superUserBuilder;
        private IPremiumUserBuilder _premiumUserBuilder;

        public UserFactoryTest()
        {
            _emailServiceMock = new Mock<IEmailService>();
            _normalUserBuilder = new NormalUserBuilder(_emailServiceMock.Object);
            _superUserBuilder = new SuperUserBuilder(_emailServiceMock.Object);
            _premiumUserBuilder= new PremiumUserBuilder(_emailServiceMock.Object);
            _userFactory = new UserFactory(_normalUserBuilder, _superUserBuilder, _premiumUserBuilder);
        }

        [Fact]
        public void When_Resolve_Is_Called__With_NormalUserType_Must_Return_NormalUserBuilder()
        {
            //Arrenge
         
            //Act
            var builder = _userFactory.Resolve(UserType.Normal);
            //Assert
            builder.Should().BeOfType<NormalUserBuilder>();
        }

        [Fact]
        public void When_Resolve_Is_Called__With_SuperUserType_Must_Return_SuperUserBuilder()
        {
            //Arrenge

            //Act
            var builder = _userFactory.Resolve(UserType.SuperUser);
            //Assert
            builder.Should().BeOfType<SuperUserBuilder>();
        }

        [Fact]
        public void When_Resolve_Is_Called__With_PremiumUserType_Must_Return_PremiumUserBuilder()
        {
            //Arrenge

            //Act
            var builder = _userFactory.Resolve(UserType.Premium);
            //Assert
            builder.Should().BeOfType<PremiumUserBuilder>();
        }
    }
}

