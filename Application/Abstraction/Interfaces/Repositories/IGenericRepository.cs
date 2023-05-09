using System.Linq.Expressions;

namespace Application.Abstraction.Repositories;

public interface IGenericRepository<T> where T : class
{
  Task<T> CreateAsync(T entity);
  Task<T> UpdateAsync(T entity);
  Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);

}
