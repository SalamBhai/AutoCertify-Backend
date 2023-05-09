using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;

namespace Application.Services.Queries.Training;

public class GetAll
{
    public record Request() : IQuery<IEnumerable<TrainingDto>>;
    
    public record Handler : IQueryHandler<Request, IEnumerable<TrainingDto>>
    {
        private readonly ITrainingRepository _trainingRepository;

        public Handler(ITrainingRepository trainingRepository) =>
        (_trainingRepository) = (trainingRepository);

        public async Task<Result<IEnumerable<TrainingDto>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var trainings = await _trainingRepository.GetAllTrainingsAsync();
            if(trainings.Count() <= 0) return await  Result<IEnumerable<TrainingDto>>.FailAsync("Trainings' retrieval returned empty data");
            return await  Result<IEnumerable<TrainingDto>>.SuccessAsync(trainings, "Trainings' retrieval successful");
           
        }
    }
}
