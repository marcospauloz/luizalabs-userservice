namespace luizalabs.UserService.Application.Interface.Repository;

using luizalabs.UserService.Domain.Core;
using System.Linq.Expressions;

public interface IBaseRepository<T> where T : Entity
{
    Task AddAsync(T obj, CancellationToken cancellationToken);

    Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);

    Task UpdateAsync(T obj, CancellationToken cancellationToken);

    Task<T> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task DeleteAsync(string id, CancellationToken cancellationToken);
}
