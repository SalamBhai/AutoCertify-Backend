using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;

namespace Application.Services.Queries;

public class GetAllWithPagination
{
    public record Request(PaginationFilter filter) : IQuery<PaginatedList<TrainingDto>>;
    
    public record Handler : IQueryHandler<Request, PaginatedList<TrainingDto>>
    {
        private readonly ITrainingRepository _trainingRepository;

        public Handler(ITrainingRepository trainingRepository) =>
        (_trainingRepository) = (trainingRepository);

        public async Task<Result<PaginatedList<TrainingDto>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var trainings = await _trainingRepository.GetTrainingsAsync(request.filter);
            if(trainings.TotalCount <= 0) return await  Result<PaginatedList<TrainingDto>>.FailAsync("Trainings' retrieval returned empty data");
            return await  Result<PaginatedList<TrainingDto>>.SuccessAsync(trainings, "Trainings' retrieval successful");
           
        }
    }
}
