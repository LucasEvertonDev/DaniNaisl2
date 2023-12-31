﻿namespace ControlServices.Application.Domain.Structure.UnitOfWork;

public interface IUnitOfWorkTransaction : IUnitOfWorkRepos
{
    Task<TRetorno> OnTransactionAsync<TRetorno>(Func<Task<TRetorno>> func, CancellationToken cancellationToken = default);
}