using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Entities;
using Core.DAL.RepositoryInterfaces;
using Core.DAL.Specifications;
using Core.Domain;

namespace Core.DAL.Repositories;

public class UserRepository : Repository<User, UserEntity>, IUserRepository
{
    public UserRepository(IDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    public async Task<User> FindByNameAsync(string login, CancellationToken cancellationToken)
    {
        var entity = await _context.SingleOrDefaultAsync(UserSpecification.ByName(login), cancellationToken);
        var result = _mapper.Map<User>(entity);
        return result;
    }
}