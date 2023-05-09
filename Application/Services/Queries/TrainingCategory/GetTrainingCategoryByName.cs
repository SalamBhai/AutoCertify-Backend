using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;

namespace Application.Services.Queries;

public class GetTrainingCategoryByName
{
    public record Request(string name) : IQuery<TrainingCategoryDto>;

    public record Handler : IQueryHandler<Request, TrainingCategoryDto>
    {
        private readonly ITrainingCategoryRepository _trainingCategoryRepository;

        public Handler(ITrainingCategoryRepository trainingCategoryRepository) =>
        (_trainingCategoryRepository) = (trainingCategoryRepository);

        public async Task<Result<TrainingCategoryDto>> Handle(Request request, CancellationToken cancellationToken)
        {
            var trainingCategory = await _trainingCategoryRepository.GetTrainingCategoryAsync(pt => pt.Name == request.name && pt.IsDeleted == false);
            if (trainingCategory is null) return await Result<TrainingCategoryDto>.FailAsync($"TrainingCategory with  name: {request.name} not found");
            var trainingCategoryReturned = new TrainingCategoryDto
            {
                Id = trainingCategory.Id,
                Name = trainingCategory.Name,
            };
            return await Result<TrainingCategoryDto>.SuccessAsync(trainingCategoryReturned, $"Training Category with  name: {request.name} successfully retrieved");


        }
    }
}
