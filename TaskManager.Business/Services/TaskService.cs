using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Dtos;
using TaskManager.Infrastructure.Models;

namespace TaskManager.Business.Services;

internal sealed class TaskService : ITaskService
{
    public TaskService()
    {
        
    }
    
    public Task CreateAsync<TModel>(TModel entity, CancellationToken cancellationToken = default) where TModel : ModelBase, new()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync<TModel>(TModel entity, CancellationToken cancellationToken = default) where TModel : ModelBase, new()
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync<TModel>(TModel entity, CancellationToken cancellationToken = default) where TModel : ModelBase, new()
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TResult?> GetEntityAsync<TResult>(Guid id, CancellationToken cancellationToken = default) where TResult : DtoBase, new()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TResult> GetEntitiesAsync<TResult>(IEnumerable<Guid> ids, CancellationToken cancellationToken = default) where TResult : DtoBase, new()
    {
        throw new NotImplementedException();
    }
}