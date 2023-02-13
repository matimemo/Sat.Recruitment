using Sat.Recruitment.Model;
using Sat.Recruitment.Services.Interfaces.BuilderInterfaces;

namespace Sat.Recruitment.Services.Interfaces
{
    public interface IUserFactory
    {
        public IUserBuilder Resolve (UserType type);
    }
}
