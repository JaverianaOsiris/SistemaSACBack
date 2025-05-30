﻿using System.Linq.Expressions;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task Create(T entity, CancellationToken cancellationToken);
    Task<IEnumerable<T?>> ReadAll(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "");
    Task<T?> ReadById(Expression<Func<T, bool>>? filter, string includeProperties = "");
    Task Update(int id, T newEntityToUpdate, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
    void DeleteByRange(IEnumerable<T> entity);
}
