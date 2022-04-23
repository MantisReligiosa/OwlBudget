using System.Threading.Tasks;
using Core.Domain;

namespace Core.ServiceInterfaces;

public interface IUserService
{
    Task<User> GetUserAsync(string username, string password);
}