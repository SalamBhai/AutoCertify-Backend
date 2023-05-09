using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;

namespace Application.Services.Queries.Participant;

public class GetAllParticipantsWithPagination
{
    public record Request(PaginationFilter filter) : IQuery<PaginatedList<ParticipantDto>>;
    
    public record Handler : IQueryHandler<Request, PaginatedList<ParticipantDto>>
    {
        private readonly IParticipantRepository _participantRepository;

        public Handler(IParticipantRepository participantRepository) =>
        (_participantRepository) = (participantRepository);

        public async Task<Result<PaginatedList<ParticipantDto>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var participants = await _participantRepository.GetParticipantsAsync(request.filter);
            if(participants.TotalCount <= 0) return await  Result<PaginatedList<ParticipantDto>>.FailAsync("Participants' retrieval returned empty data");
            return await  Result<PaginatedList<ParticipantDto>>.SuccessAsync(participants, "Participants' retrieval successful");
           
        }
    }
}
