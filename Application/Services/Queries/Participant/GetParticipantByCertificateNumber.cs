using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;

namespace Application.Services.Queries.Participant;

public class GetParticipantByCertificateNumber
{
    public record Request(string certificateNumber) : IQuery<ParticipantDto>;

    public record Handler : IQueryHandler<Request, ParticipantDto>
    {
        private readonly IParticipantRepository _participantRepository;

        public Handler(IParticipantRepository participantRepository) =>
        (_participantRepository) = (participantRepository);

        public async Task<Result<ParticipantDto>> Handle(Request request, CancellationToken cancellationToken)
        {
            var participant = await _participantRepository.GetParticipantAsync(pt => pt.CertificateNumber == request.certificateNumber && pt.IsDeleted == false);
            if (participant is null) return await Result<ParticipantDto>.FailAsync($"Participant with certificate number: {request.certificateNumber} not found");
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
            return await Result<ParticipantDto>.SuccessAsync(participantReturned, $"Participant with certificate number: {request.certificateNumber} successfully retrieved");
            

        }
    }
}
