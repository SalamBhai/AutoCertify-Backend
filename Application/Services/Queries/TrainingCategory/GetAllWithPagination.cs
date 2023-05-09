using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.DTOs;
using Application.Wrapper;

namespace Application.Services.Queries;

public class GetAllTrainingCategoriesWithPagination
{
    public record Request(PaginationFilter filter) : IQuery<PaginatedList<TrainingCategoryDto>>;
    
    public record Handler : IQueryHandler<Request, PaginatedList<TrainingCategoryDto>>
    {
        private readonly ITrainingCategoryRepository _trainingCategoryRepository;

        public Handler(ITrainingCategoryRepository trainingCategoryRepository) =>
        (_trainingCategoryRepository) = (trainingCategoryRepository);

        public async Task<Result<PaginatedList<TrainingCategoryDto>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var trainingCategories = await _trainingCategoryRepository.GetTrainingCategoriesAsync(request.filter);
            if(trainingCategories.TotalCount <= 0) return await  Result<PaginatedList<TrainingCategoryDto>>.FailAsync("Training Categories' retrieval returned empty data");
            return await  Result<PaginatedList<TrainingCategoryDto>>.SuccessAsync(trainingCategories, "Training Categories' retrieval successful");
           
        }
    }
}
