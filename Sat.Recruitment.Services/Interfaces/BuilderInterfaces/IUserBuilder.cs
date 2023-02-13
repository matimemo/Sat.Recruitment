using Sat.Recruitment.Model;
using Sat.Recruitment.Model.Dtos;

namespace Sat.Recruitment.Services.Interfaces.BuilderInterfaces
{
    public interface IUserBuilder
    {
        public User Build(UserDto userDto);
    }
}