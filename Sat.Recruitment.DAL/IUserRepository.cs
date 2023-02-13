using System.Threading.Tasks;
using Sat.Recruitment.Model;

namespace Sat.Recruitment.DAL
{
    public interface IUserRepository
    {
        public Task<bool> Exists(User user);
        public Task Add(User user);
    }
}
