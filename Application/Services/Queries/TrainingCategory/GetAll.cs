using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;

namespace Application.Services.Queries;

public class GetAllTrainingCategories
{
    public record Request() : IQuery<IEnumerable<TrainingCategoryDto>>;
    
    public record Handler : IQueryHandler<Request, IEnumerable<TrainingCategoryDto>>
    {
        private readonly ITrainingCategoryRepository _trainingCategoryRepository;

        public Handler(ITrainingCategoryRepository trainingCategoryRepository) =>
        (_trainingCategoryRepository) = (trainingCategoryRepository);

        public async Task<Result<IEnumerable<TrainingCategoryDto>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var trainingCategories = await _trainingCategoryRepository.GetAllTrainingCategoriesAsync();
            if(trainingCategories.Count() <= 0) return await  Result<IEnumerable<TrainingCategoryDto>>.FailAsync("Training Categories' retrieval returned empty data");
            return await  Result<IEnumerable<TrainingCategoryDto>>.SuccessAsync(trainingCategories, "Training Categories' retrieval successful");
           
        }
    }
}
