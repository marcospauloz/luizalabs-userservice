namespace luizalabs.UserService.Infrastructure;

using System.Linq.Expressions;
using Application.Interface.Repository;
using Domain.Core;
using MongoDB.Driver;

public class MongoBaseRepository<T> : IBaseRepository<T> where T : Entity
{
    private readonly IMongoClient _client;

    private readonly IMongoCollection<T> _collection;

    public MongoBaseRepository(IMongoClient client)
    {
        _client = client;

        _collection = client
            .GetDatabase("luizalabsUserService")
            .GetCollection<T>(typeof(T).Name);
    }

    public virtual async Task AddAsync(T obj, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(obj, cancellationToken: cancellationToken);
    }

    public virtual async Task UpdateAsync(T obj, CancellationToken cancellationToken)
    {
        await _collection.ReplaceOneAsync(x => x.Id == obj.Id, obj, cancellationToken: cancellationToken);
    }

    public virtual async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _collection.Find(Builders<T>.Filter.Eq(x => x.Id, id.ToString()))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _collection.DeleteOneAsync(x => x.Id == id.ToString(), cancellationToken);
    }

    public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
    {
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public virtual Task<IClientSessionHandle> StartSessionAsync(CancellationToken cancellationToken)
    {
        return _client.StartSessionAsync(cancellationToken: cancellationToken);
    }
}