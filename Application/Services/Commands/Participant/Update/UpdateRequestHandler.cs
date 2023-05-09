using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;

namespace Application.Services.Commands;

public record UpdateRequestHandler : ICommandHandler<UpdateRequest, string>
{
    private readonly IParticipantRepository _participantRepository;

    public UpdateRequestHandler(IParticipantRepository participantRepository) =>
    (_participantRepository) = (participantRepository);

    public async Task<Result<string>> Handle(UpdateRequest request, CancellationToken cancellationToken)
    {
        var participant =  await _participantRepository.GetParticipantAsync(pt => pt.FullName == request.existingFullName
        ,false);
        if (participant is null) 
        return new Result<string>
            {
                Messages = new List<string> {
                $"Participant With {request.existingFullName} could not be found"},
                Succeeded = false,

            };

        var updatedParticipant = participant.Update(request.LastName, request.MiddleName, request.FirstName);
        var savedResponse = await _participantRepository.UpdateAsync(updatedParticipant);
        return new Result<string>
        {
            Messages = new List<string> {
                $"Update request for participant: {request.existingFullName} was successful",},
            Succeeded = true,
        };

    }
}
