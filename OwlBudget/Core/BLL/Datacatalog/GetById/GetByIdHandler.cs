using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using MediatR;

namespace Core.BLL.DataCatalog.GetById;

public class GetByIdHandler<T> : IRequestHandler<GetByIdRequest<T>, T>
    where T : NamedIdentity

{
    private readonly IRepository<T> _repository;

    public GetByIdHandler(IRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<T> Handle(GetByIdRequest<T> request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id, cancellationToken);
    }
}