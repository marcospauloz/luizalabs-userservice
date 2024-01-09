namespace Dotnet.MiniJira.Infrastructure;

using luizalabs.UserService.Application.Interface.Repository;
using luizalabs.UserService.Domain.Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;


public class MongoBaseRepository<T> : IBaseRepository<T> where T : Entity
{
    protected readonly IMongoClient Client;

    protected readonly IMongoCollection<T> Collection;

    public MongoBaseRepository(IMongoClient client)
    {
        Client = client;

        Collection = client
            .GetDatabase("luizalabsUserService")
            .GetCollection<T>(typeof(T).Name);
    }

    public virtual async Task AddAsync(T obj, CancellationToken cancellationToken)
    {
        await Collection.InsertOneAsync(obj, cancellationToken: cancellationToken);
    }

    public virtual async Task UpdateAsync(T obj, CancellationToken cancellationToken)
    {
        await Collection.ReplaceOneAsync(x => x.Id == obj.Id, obj, cancellationToken: cancellationToken);
    }

    public virtual async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await Collection.Find(Builders<T>.Filter.Eq(x => x.Id, id.ToString()))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await Collection.DeleteOneAsync(x => x.Id == id.ToString(), cancellationToken);
    }

    public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
    {
        return await Collection.Find(filter).ToListAsync(cancellationToken);
    }

    public virtual Task<IClientSessionHandle> StartSessionAsync(CancellationToken cancellationToken)
    {
        return Client.StartSessionAsync(cancellationToken: cancellationToken);
    }
}