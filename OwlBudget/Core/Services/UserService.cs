using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Core.ServiceInterfaces;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly ICryptoService _cryptoService;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, ICryptoService cryptoService)
    {
        _userRepository = userRepository;
        _cryptoService = cryptoService;
    }

    public async Task<User> GetUserAsync(string username, string password)
    {
        var user = await _userRepository.FindByNameAsync(username, CancellationToken.None);
        var hash = _cryptoService.Hash(password);
        return !hash.Equals(user.PasswordHash) ? null : user;
    }
}