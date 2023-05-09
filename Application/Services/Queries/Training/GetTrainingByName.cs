using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;

namespace Application.Services.Queries;

public class GetTrainingByName
{
    public record Request(string name) : IQuery<TrainingDto>;

    public record Handler : IQueryHandler<Request, TrainingDto>
    {
        private readonly ITrainingRepository _trainingRepository;

        public Handler(ITrainingRepository trainingRepository) =>
        (_trainingRepository) = (trainingRepository);

        public async Task<Result<TrainingDto>> Handle(Request request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetTrainingAsync(pt => pt.TrainingName == request.name && pt.IsDeleted == false);
            if (training is null) return await Result<TrainingDto>.FailAsync($"Training with  name: {request.name} not found");
            var trainingReturned = new TrainingDto
            {

                DateOfCertificateIssuance = training.DateOfCertificateIssuance,
                TrainingName = training.TrainingName,
                TrainingCategoryName = training.TrainingCategory.Name,
            };
            return await Result<TrainingDto>.SuccessAsync(trainingReturned, $"Training with name: {request.name} successfully retrieved");


        }
    }
}
