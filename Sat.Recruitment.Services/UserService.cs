using Sat.Recruitment.Model;
using System.Threading.Tasks;
using Sat.Recruitment.DAL;
using Sat.Recruitment.Services.Interfaces;

namespace Sat.Recruitment.Services
{
    public class UserService : IUserService 
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> AddIfNotExist(User user)
        {
            if (!await _userRepository.Exists(user))
            {
                await _userRepository.Add(user);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
