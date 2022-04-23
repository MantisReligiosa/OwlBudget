using System.Threading;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DAL.RepositoryInterfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> FindByNameAsync(string login, CancellationToken cancellationToken);
}