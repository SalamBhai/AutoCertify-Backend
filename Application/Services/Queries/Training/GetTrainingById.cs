using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;

namespace Application.Services.Queries.Training;

public class GetTrainingById
{
    public record Request(Guid id) : IQuery<TrainingDto>;

    public record Handler : IQueryHandler<Request, TrainingDto>
    {
        private readonly ITrainingRepository _trainingRepository;

        public Handler(ITrainingRepository trainingRepository) =>
        (_trainingRepository) = (trainingRepository);

        public async Task<Result<TrainingDto>> Handle(Request request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetTrainingAsync(request.id);
            if (training is null) return await Result<TrainingDto>.FailAsync($"Training with id: {request.id} not found");
            var trainingReturned = new TrainingDto
            {

                TrainingName = training.TrainingName,
                Id = training.Id,
                DateOfCertificateIssuance = training.DateOfCertificateIssuance,
                TrainingCategoryName = training.TrainingCategory.Name,

            };
            return await Result<TrainingDto>.SuccessAsync(trainingReturned, $"Training with id: {request.id} successfully retrieved");


        }
    }
}
