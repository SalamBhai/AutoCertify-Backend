using System.Linq.Expressions;
using Application.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected ApplicationContext _context;

    public GenericRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> exp)         
    {
        var result =  await _context.Set<T>().AsNoTrackingWithIdentityResolution().AnyAsync(exp);
        return result;
    }

    
}
