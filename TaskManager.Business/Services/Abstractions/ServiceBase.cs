using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Core.UnitOfWork;

namespace TaskManager.Business.Services.Abstractions;

internal abstract class ServiceBase : IServiceBase
{
    protected IUnitOfWork UnitOfWork { get; }

    protected ServiceBase(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}