using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.Wrapper;

namespace Application.Services.Commands;

public record DeleteTrainingCategoryHandler : ICommandHandler<DeleteTrainingCategoryRequest, string>
{
    private readonly ITrainingCategoryRepository _trainingCategoryRepository;

    public DeleteTrainingCategoryHandler(ITrainingCategoryRepository trainingCategoryRepository)
    => (_trainingCategoryRepository) = (trainingCategoryRepository);
    
    public async Task<Result<string>> Handle(DeleteTrainingCategoryRequest request, CancellationToken cancellationToken)
    {
        var trainingCategory = await _trainingCategoryRepository.GetTrainingCategoryAsync(pt => pt.Name == request.trainingCategoryName && pt.IsDeleted == false, false);
        if (trainingCategory is null) return new Result<string>
        {
            Messages = new List<string> {
                $"Training Category with {request.trainingCategoryName} could not be found"
            },
            Succeeded = false,
            
        };
        var trainingCategoryReturned =  trainingCategory.Delete();
        var savedResponse = await _trainingCategoryRepository.UpdateAsync(trainingCategoryReturned);
         return new Result<string>
        {
            Messages = new List<string> {
                $"Delete Request For Training Category with {request.trainingCategoryName} was successful"
            },
            Succeeded = true,
            
        };
    }
}
