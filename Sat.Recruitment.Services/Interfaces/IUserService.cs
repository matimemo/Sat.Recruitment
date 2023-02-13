using System.Threading.Tasks;
using Sat.Recruitment.Model;

namespace Sat.Recruitment.Services.Interfaces
{
    public interface IUserService
    {
        public Task<bool> AddIfNotExist(User user);
    }
}