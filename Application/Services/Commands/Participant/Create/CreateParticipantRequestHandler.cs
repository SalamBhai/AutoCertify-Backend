
using Application.DTOs;
using Application.Exceptions;
using Application.Abstraction.Repositories;
using Application.Wrapper;
using Application.Abstraction;
using Application.Abstraction.Interfaces.Repositories.Utilities;

namespace Application.Services.Commands;

public record CreateParticipantRequestHandler : ICommandHandler<CreateParticipantRequest, bool>
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IEventIncrementer _eventIncrementer;

    public CreateParticipantRequestHandler(IParticipantRepository participantRepository, IEventIncrementer eventIncrementer)
    => (_participantRepository, _eventIncrementer) = (participantRepository, eventIncrementer);


    public async Task<Result<bool>> Handle(CreateParticipantRequest request, CancellationToken cancellationToken)
    {
        var participantExists = await _participantRepository.ExistsAsync(p => p.FullName == $"{request.FirstName} {request.LastName}" && p.TrainingId == request.TrainingId && p.IsDeleted == false);
        if (participantExists)
        {
            return new Result<bool>
            {
                Messages = new List<string> {
                $"Participant With {request.FirstName} {request.LastName} already exists."},
                Succeeded = false,

            };
        }
        string certificateNumber = await GenerateCertificateNumber();
        var participant = new Domain.Entities.Participant(false, $"{request.FirstName} {request.LastName}", request.FirstName,
        request.LastName, request.MiddleName, certificateNumber, request.TrainingId);
        var saveResponse = (await _participantRepository.CreateAsync(participant));

        if (saveResponse.FullName != $"{request.FirstName} {request.LastName}")
            return new Result<bool>
            {
                Messages = new List<string> {
                $"Registration request for participant failed."},
                Succeeded = false,

            };
        return new Result<bool>
        {
            Messages = new List<string> {
                $"Registration request for participant: {saveResponse.FullName} was successful",
                $"Here is the id: {saveResponse.Id}",
                $"Certificate Number: {certificateNumber}"},

            Succeeded = true,

        };
    }

    private async Task<string> GenerateCertificateNumber()
    {
        string format = "TC00000";
        var lastDbCertificateNumber = await _participantRepository.GetLastParticipantCertificateNumber();
        if (lastDbCertificateNumber == string.Empty)
        {
            lastDbCertificateNumber = format;
        }
        var currentEventIdentifier = _eventIncrementer.EventSequence(lastDbCertificateNumber);
        return currentEventIdentifier;
    }
}
