using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;

namespace Application.Services.Queries.Participant;

public class GetParticipantByName
{
    public record Request(string fullName) : IQuery<ParticipantDto>;

    public record Handler : IQueryHandler<Request, ParticipantDto>
    {
        private readonly IParticipantRepository _participantRepository;

        public Handler(IParticipantRepository participantRepository) =>
        (_participantRepository) = (participantRepository);

        public async Task<Result<ParticipantDto>> Handle(Request request, CancellationToken cancellationToken)
        {
            var participant = await _participantRepository.GetParticipantAsync(pt => pt.FullName == request.fullName && pt.IsDeleted == false);
            if (participant is null) return await Result<ParticipantDto>.FailAsync($"Participant with name: {request.fullName} not found");
            var participantReturned = new ParticipantDto
            {
                CertificateNumber = participant.CertificateNumber,
                FirstName = participant.FirstName,
                LastName = participant.LastName,
                FullName = participant.FullName,
                Id = participant.Id,
                TrainingId = participant.TrainingId,
                MiddleName = participant.MiddleName,
                Training = new TrainingDto
                {
                    DateOfCertificateIssuance = participant.Training.DateOfCertificateIssuance,
                    TrainingName = participant.Training.TrainingName,

                },
                TrainingCategory = new TrainingCategoryDto
                {
                    Name = participant.Training.TrainingCategory.Name
                }
            };
            return await Result<ParticipantDto>.SuccessAsync(participantReturned, $"Participant with name: {request.fullName} successfully retrieved");
            

        }
    }
}
