using Sat.Recruitment.Model;
using Sat.Recruitment.Services.Interfaces;
using Sat.Recruitment.Services.Interfaces.BuilderInterfaces;

namespace Sat.Recruitment.Services
{
    public class UserFactory : IUserFactory
    {
        private readonly INormalUserBuilder _normalUserBuilder;
        private readonly IPremiumUserBuilder _premiumUserBuilder;
        private readonly ISuperUserBuilder _superUserBuilder;
        public UserFactory(INormalUserBuilder normalUserBuilder,
            ISuperUserBuilder superUserBuilder,
            IPremiumUserBuilder premiumUserBuilder)
        {
            _normalUserBuilder = normalUserBuilder;
            _superUserBuilder = superUserBuilder;
            _premiumUserBuilder = premiumUserBuilder;
        }


        public IUserBuilder Resolve(UserType type)
        {
            switch (type)
            {
                case UserType.Normal:
                    return _normalUserBuilder;
                case UserType.Premium:
                    return _premiumUserBuilder;
                case UserType.SuperUser:
                    return _superUserBuilder;
                default:
                    return _normalUserBuilder;
            }
        }
    }
}
