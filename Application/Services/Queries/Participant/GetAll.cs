using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;

namespace Application.Services.Queries.Participant;

public class GetAllParticipants
{
    public record Request() : IQuery<IEnumerable<ParticipantDto>>;
    
    public record Handler : IQueryHandler<Request, IEnumerable<ParticipantDto>>
    {
        private readonly IParticipantRepository _participantRepository;

        public Handler(IParticipantRepository participantRepository) =>
        (_participantRepository) = (participantRepository);

        public async Task<Result<IEnumerable<ParticipantDto>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var participants = await _participantRepository.GetAllParticipantsAsync();
            if(participants.Count() <= 0) return await  Result<IEnumerable<ParticipantDto>>.FailAsync("Participants' retrieval returned empty data");
            return await  Result<IEnumerable<ParticipantDto>>.SuccessAsync(participants, "Participants' retrieval successful");
           
        }
    }
}
