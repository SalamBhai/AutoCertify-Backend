using System.Linq.Expressions;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Extensions;

namespace Persistence.Repositories;

public class TrainingRepository : GenericRepository<Training>, ITrainingRepository
{
    public TrainingRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TrainingDto>> GetAllTrainingsAsync()
    {
       return await _context.Trainings
       .Include(training => training.TrainingCategory)
       .Where(p => p.IsDeleted == false)
       .AsNoTrackingWithIdentityResolution().
       Select(p => new TrainingDto
        {
           
            TrainingName = p.TrainingName,
            Id = p.Id,
            DateOfCertificateIssuance = p.DateOfCertificateIssuance,
            TrainingCategoryName = p.TrainingCategory.Name

        })
       .ToListAsync();
    }

    public async  Task<Training> GetTrainingAsync(Expression<Func<Training, bool>> expression, bool asNoTracking = true)
    {
        return asNoTracking ?  await _context.Trainings
       .Include(training => training.TrainingCategory)
       .Include(tr => tr.Participants)
       .AsNoTrackingWithIdentityResolution()
       .Where(expression).SingleOrDefaultAsync() : await _context.Trainings
       .Include(training => training.TrainingCategory)
       .Include(tr => tr.Participants)
       .Where(expression).SingleOrDefaultAsync();
    }

    public async Task<Training> GetTrainingAsync(Guid id, bool asNoTracking = true)
    {
        return asNoTracking? await _context.Trainings
       .Include(training => training.TrainingCategory)
       .Include(tr => tr.Participants)
       .AsNoTrackingWithIdentityResolution()
       .Where(tr => tr.Id == id && tr.IsDeleted == false).SingleOrDefaultAsync() :
       await _context.Trainings
       .Include(training => training.TrainingCategory)
       .Include(tr => tr.Participants)
       .Where(tr => tr.Id == id && tr.IsDeleted == false).SingleOrDefaultAsync();
    }

    public async Task<PaginatedList<TrainingDto>> GetTrainingsAsync(PaginationFilter filter)
    {
        return await _context.Trainings.Include(p => p.TrainingCategory).
        Include(p => p.Participants).Where(p => p.IsDeleted == false).AsNoTrackingWithIdentityResolution().Select(p => new TrainingDto
        {
           
            TrainingName = p.TrainingName,
            Id = p.Id,
            DateOfCertificateIssuance = p.DateOfCertificateIssuance,
            TrainingCategoryName = p.TrainingCategory.Name,


        }).ToPaginatedListAsync(filter.PageNumber, filter.PageSize);
       
    }

    public async Task<IEnumerable<TrainingDto>> GetTrainingsAsync(Expression<Func<Training, bool>> expression)
    {
       return await _context.Trainings
       .Include(training => training.TrainingCategory)
       .AsNoTrackingWithIdentityResolution()
       .Where(expression)
       .Select(p => new TrainingDto
        {
           
            TrainingName = p.TrainingName,
            Id = p.Id,
            DateOfCertificateIssuance = p.DateOfCertificateIssuance,
            TrainingCategoryName = p.TrainingCategory.Name,

        })
       .ToListAsync();
    }

    public async Task<PaginatedList<TrainingDto>> GetTrainingsAsync(Expression<Func<Training, bool>> expression, PaginationFilter filter)
    {
        return await _context.Trainings.Include(p => p.TrainingCategory).
        Include(p => p.Participants).Where(expression).AsNoTrackingWithIdentityResolution().Select(p => new TrainingDto
        {
           
            TrainingName = p.TrainingName,
            Id = p.Id,
            DateOfCertificateIssuance = p.DateOfCertificateIssuance,
            
            TrainingCategoryName = p.TrainingCategory.Name,


        }).ToPaginatedListAsync(filter.PageNumber, filter.PageSize);
    }
}
