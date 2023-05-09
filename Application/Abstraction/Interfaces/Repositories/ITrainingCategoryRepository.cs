using System.Linq.Expressions;
using Application.DTOs;
using Application.Wrapper;
using Domain.Entities;

namespace Application.Abstraction.Repositories;

public interface ITrainingCategoryRepository : IGenericRepository<TrainingCategory>
{
   Task<TrainingCategory> GetTrainingCategoryAsync(Guid id, bool asNoTracking = true);
    Task<PaginatedList<TrainingCategoryDto>> GetTrainingCategoriesAsync(PaginationFilter filter);
    Task<PaginatedList<TrainingCategoryDto>> GetTrainingCategoriesAsync(Expression<Func<TrainingCategory, bool>> expression, PaginationFilter filter);
    Task<TrainingCategory> GetTrainingCategoryAsync(Expression<Func<TrainingCategory, bool>> expression, bool asNoTracking = true);
    Task<IEnumerable<TrainingCategory>> GetTrainingCategoriesAsync(Expression<Func<TrainingCategory, bool>> expression);
    Task<IEnumerable<TrainingCategoryDto>> GetAllTrainingCategoriesAsync(); 
}
