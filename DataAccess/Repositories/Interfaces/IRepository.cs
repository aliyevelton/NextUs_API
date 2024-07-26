using Core.Entities.Common;
using System.Linq.Expressions;

namespace DataAccess.Repositories.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync(params string[] includes);
    Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>> expression, params string[] includes);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression, params string[] includes);
    Task<int> SaveAsync();
}
