using System.Linq.Expressions;
using Application.DTOs;
using Application.Wrapper;
using Domain.Entities;

namespace Application.Abstraction.Repositories;

public interface ITrainingRepository : IGenericRepository<Training>
{
   Task<Training> GetTrainingAsync(Guid id, bool asNoTracking = true);
    Task<PaginatedList<TrainingDto>> GetTrainingsAsync(PaginationFilter filter);
    Task<Training> GetTrainingAsync(Expression<Func<Training, bool>> expression, bool asNoTracking = true);
    Task<IEnumerable<TrainingDto>> GetTrainingsAsync(Expression<Func<Training, bool>> expression);
    Task<PaginatedList<TrainingDto>> GetTrainingsAsync(Expression<Func<Training, bool>> expression, PaginationFilter filter);
    Task<IEnumerable<TrainingDto>> GetAllTrainingsAsync();
}
