using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.Wrapper;

namespace Application.Services.Commands;

public record DeleteHandler : ICommandHandler<DeleteRequest, string>
{
    private readonly IParticipantRepository _participantRepository;

    public DeleteHandler(IParticipantRepository participantRepository)
    => (_participantRepository) = (participantRepository);
    
    public async Task<Result<string>> Handle(DeleteRequest request, CancellationToken cancellationToken)
    {
        var participant = await _participantRepository.GetParticipantAsync(pt => pt.FullName == request.fullName && pt.IsDeleted == false, false);
        if (participant is null)   return new Result<string>
            {
                Messages = new List<string> {
                $"Participant With {request.fullName} could not be found"},
                Succeeded = false,

            };
        var participantReturned =  participant.Delete();
        var savedResponse = await _participantRepository.UpdateAsync(participant);
        return new Result<string>
        {
            Messages = new List<string> {
                $"Delete request for participant: {request.fullName} was successful",
                },
            Succeeded = true,

        };
    }
}
