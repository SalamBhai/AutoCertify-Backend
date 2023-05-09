using System.Linq.Expressions;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Extensions;

namespace Persistence.Repositories;

public class ParticipantRepository : GenericRepository<Participant>, IParticipantRepository
{
    public ParticipantRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ParticipantDto>> GetAllParticipantsAsync()
    {
        var participants = await _context.Participants.
        AsNoTrackingWithIdentityResolution().
        Include(p => p.Training).ThenInclude(p => p.TrainingCategory).Where(p => p.IsDeleted == false).Select(p => new ParticipantDto
        {
            CertificateNumber = p.CertificateNumber,
            FirstName = p.FirstName,
            LastName = p.LastName,
            FullName = p.FullName,
            Id = p.Id,
            TrainingId = p.TrainingId,
            MiddleName = p.MiddleName,
            Training = new TrainingDto
            {
                DateOfCertificateIssuance = p.Training.DateOfCertificateIssuance,
                
                TrainingName = p.Training.TrainingName,

            },
            TrainingCategory = new TrainingCategoryDto
            {
                Name = p.Training.TrainingCategory.Name
            }

        }).ToListAsync();
        return participants;
    }

    public async Task<Participant> GetParticipantAsync(Guid id , bool asNoTracking = true)
    {
     return asNoTracking ?  await _context.Participants.AsNoTrackingWithIdentityResolution().Include(p => p.Training).
        ThenInclude(p => p.TrainingCategory).
      Where(p => p.Id == id &&  p.IsDeleted == false).FirstOrDefaultAsync() : 
      await _context.Participants.AsNoTrackingWithIdentityResolution().Include(p => p.Training).
        ThenInclude(p => p.TrainingCategory).
      Where(p => p.Id == id &&  p.IsDeleted == false).FirstOrDefaultAsync();
    }

    public async Task<Participant> GetParticipantAsync(Expression<Func<Participant, bool>> expression, bool asNoTracking = true)
    {
        return asNoTracking ?  await _context.Participants.AsNoTrackingWithIdentityResolution().Include(p => p.Training).
        ThenInclude(p => p.TrainingCategory).
      Where(expression).FirstOrDefaultAsync() : 
      await _context.Participants.Include(p => p.Training).
        ThenInclude(p => p.TrainingCategory).
      Where(expression).FirstOrDefaultAsync();
     
    }

   public async Task<PaginatedList<ParticipantDto>> GetParticipantsAsync(Expression<Func<Participant, bool>> expression,  PaginationFilter filter) 
    {
        return await _context.Participants.AsNoTrackingWithIdentityResolution().Include(p => p.Training).ThenInclude(p => p.TrainingCategory).Where(expression).Select(p => new ParticipantDto
        {
            CertificateNumber = p.CertificateNumber,
            FirstName = p.FirstName,
            LastName = p.LastName,
            FullName = p.FullName,
            Id = p.Id,
            TrainingId = p.TrainingId,
            MiddleName = p.MiddleName,
            Training = new TrainingDto
            {
                DateOfCertificateIssuance = p.Training.DateOfCertificateIssuance,
                
                TrainingName = p.Training.TrainingName,

            },
            TrainingCategory = new TrainingCategoryDto
            {
                Name = p.Training.TrainingCategory.Name
            }

        }).ToPaginatedListAsync(filter.PageNumber, filter.PageSize, filter.OrderBy[0] = null);
       
    }
   public async Task<PaginatedList<ParticipantDto>> GetParticipantsAsync(PaginationFilter filter) 
    {
        return await _context.Participants.AsNoTrackingWithIdentityResolution().
        Include(p => p.Training).ThenInclude(p => p.TrainingCategory).Where(p =>  p.IsDeleted == false).Select(p => new ParticipantDto
        {
            CertificateNumber = p.CertificateNumber,
            FirstName = p.FirstName,
            LastName = p.LastName,
            FullName = p.FullName,
            Id = p.Id,
            TrainingId = p.TrainingId,
            MiddleName = p.MiddleName,
            Training = new TrainingDto
            {
                DateOfCertificateIssuance = p.Training.DateOfCertificateIssuance,
                
                TrainingName = p.Training.TrainingName,

            },
            TrainingCategory = new TrainingCategoryDto
            {
                Name = p.Training.TrainingCategory.Name
            }

        }).AsQueryable().ToPaginatedListAsync(filter.PageNumber, filter.PageSize, filter.OrderBy[0] = null);
       
    }
    public async Task<IEnumerable<ParticipantDto>> GetParticipantsAsync(Expression<Func<Participant, bool>> expression)
    {
      return await _context.Participants
      .AsNoTrackingWithIdentityResolution().Where(expression).Select(p => new ParticipantDto
        {
            CertificateNumber = p.CertificateNumber,
            FirstName = p.FirstName,
            LastName = p.LastName,
            FullName = p.FullName,
            Id = p.Id,
            TrainingId = p.TrainingId,
            MiddleName = p.MiddleName,
            Training = new TrainingDto
            {
                DateOfCertificateIssuance = p.Training.DateOfCertificateIssuance,
                
                TrainingName = p.Training.TrainingName,

            },
            TrainingCategory = new TrainingCategoryDto
            {
                Name = p.Training.TrainingCategory.Name
            }

        }).ToListAsync();
    }
     public async Task<string> GetLastParticipantCertificateNumber()
        {
            var participant = await _context.Participants.Include(x=>x.Training).OrderBy(x => x.CertificateNumber).LastOrDefaultAsync();
            if (participant == null) return "";
            return participant.CertificateNumber;
        }
}
