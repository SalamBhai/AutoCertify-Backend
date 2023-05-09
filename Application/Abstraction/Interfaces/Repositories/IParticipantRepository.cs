using System.Linq.Expressions;
using Application.DTOs;
using Application.Wrapper;
using Domain.Entities;

namespace Application.Abstraction.Repositories;

public interface IParticipantRepository : IGenericRepository<Participant>
{
    Task<Participant> GetParticipantAsync(Guid id, bool asNoTracking = true);
    Task<PaginatedList<ParticipantDto>> GetParticipantsAsync(PaginationFilter filter);
    Task<PaginatedList<ParticipantDto>> GetParticipantsAsync(Expression<Func<Participant, bool>> expression, PaginationFilter filter);
    Task<Participant> GetParticipantAsync(Expression<Func<Participant, bool>> expression, bool asNoTracking = true);
    Task<IEnumerable<ParticipantDto>> GetParticipantsAsync(Expression<Func<Participant, bool>> expression);
    Task<IEnumerable<ParticipantDto>> GetAllParticipantsAsync();
    Task<string> GetLastParticipantCertificateNumber();
}
