using System.Linq.Expressions;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Extensions;

namespace Persistence.Repositories;

public class TrainingCategoryRepository : GenericRepository<TrainingCategory>, ITrainingCategoryRepository
{
    public TrainingCategoryRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TrainingCategoryDto>> GetAllTrainingCategoriesAsync()
    {
       return await _context.TrainingCategories.Where(p =>  p.IsDeleted == false).Select(p => new TrainingCategoryDto
        {
           
            Name = p.Name,
            Id = p.Id,

        }).AsNoTrackingWithIdentityResolution().ToListAsync();
    }

    public async Task<PaginatedList<TrainingCategoryDto>> GetTrainingCategoriesAsync(PaginationFilter filter)
    {
        return await _context.TrainingCategories.Where(p =>  p.IsDeleted == false).Select(p => new TrainingCategoryDto
        {
           
            Name = p.Name,
            Id = p.Id,

        }).AsNoTrackingWithIdentityResolution().ToPaginatedListAsync(filter.PageNumber, filter.PageSize);
       
    }
    public async Task<PaginatedList<TrainingCategoryDto>> GetTrainingCategoriesAsync(Expression<Func<TrainingCategory, bool>> expression, PaginationFilter filter)
    {
        return await _context.TrainingCategories.Where(p =>  p.IsDeleted == false).Select(p => new TrainingCategoryDto
        {
           
            Name = p.Name,
            Id = p.Id,

        }).AsNoTrackingWithIdentityResolution().ToPaginatedListAsync(filter.PageNumber, filter.PageSize);
       
    }

    public async Task<IEnumerable<TrainingCategory>> GetTrainingCategoriesAsync(Expression<Func<TrainingCategory, bool>> expression)
    {
       return await _context.TrainingCategories.AsNoTrackingWithIdentityResolution().Where(p =>  p.IsDeleted == false).ToListAsync();
    }

    public async Task<TrainingCategory> GetTrainingCategoryAsync(Guid id, bool asNoTracking = true)
    {
       return  asNoTracking ? await _context.TrainingCategories.AsNoTrackingWithIdentityResolution().
       Where(p => p.Id == id && p.IsDeleted == false).SingleOrDefaultAsync() : await _context.TrainingCategories.
       Include(p => p.Trainings ).
       Where(p => p.Id == id && p.IsDeleted == false).SingleOrDefaultAsync();
    }

    public async Task<TrainingCategory> GetTrainingCategoryAsync(Expression<Func<TrainingCategory, bool>> expression, bool asNoTracking = true)
    {
       return asNoTracking ? await _context.TrainingCategories.AsNoTrackingWithIdentityResolution().
       Where(expression).SingleOrDefaultAsync() :
       await _context.TrainingCategories.AsNoTrackingWithIdentityResolution()
       .
       Where(expression).SingleOrDefaultAsync();
        
    }
}
