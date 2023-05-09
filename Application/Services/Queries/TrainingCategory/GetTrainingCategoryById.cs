using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;

namespace Application.Services.Queries;

public class GetTrainingCategoryById
{
    public record Request(Guid id) : IQuery<TrainingCategoryDto>;

    public record Handler : IQueryHandler<Request, TrainingCategoryDto>
    {
        private readonly ITrainingCategoryRepository _trainingCategoryRepository;

        public Handler(ITrainingCategoryRepository trainingCategoryRepository) =>
        (_trainingCategoryRepository) = (trainingCategoryRepository);

        public async Task<Result<TrainingCategoryDto>> Handle(Request request, CancellationToken cancellationToken)
        {
            var trainingCategory = await _trainingCategoryRepository.GetTrainingCategoryAsync(request.id);
            if (trainingCategory is null) return await Result<TrainingCategoryDto>.FailAsync($"TrainingCategory with id: {request.id} not found");
            var trainingCategoryReturned = new TrainingCategoryDto
            {
                Name = trainingCategory.Name,
                Id = trainingCategory.Id,
            };
            return await Result<TrainingCategoryDto>.SuccessAsync(trainingCategoryReturned, $"TrainingCategory with id: {request.id} successfully retrieved");


        }
    }
}
