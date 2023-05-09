using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.Wrapper;

namespace Application.Services.Commands;

public record UpdateTrainingCategoryRequestHandler : ICommandHandler<UpdateTrainingCategoryRequest, string>
{
    private readonly ITrainingCategoryRepository _trainingCategoryRepository;

    public UpdateTrainingCategoryRequestHandler(ITrainingCategoryRepository trainingCategoryRepository) =>
    (_trainingCategoryRepository) = (trainingCategoryRepository);

    public async Task<Result<string>> Handle(UpdateTrainingCategoryRequest request, CancellationToken cancellationToken)
    {
        var trainingCategory =  await _trainingCategoryRepository.GetTrainingCategoryAsync(pt => pt.Name == request.existingName
        ,false);
        if (trainingCategory is null) return new Result<string>
        {
            Messages = new List<string> {
                $"Training Category with {request.existingName} could not be found"
            },
            Succeeded = false,
            
        };
        var updatedTrainingCategory = trainingCategory.Update(request.name);
        var savedResponse = await _trainingCategoryRepository.UpdateAsync(updatedTrainingCategory);
         return new Result<string>
        {
            Messages = new List<string> {
                $"Update request for TrainingCategory: {request.name} was successful"
            },
            Succeeded = true,
            
        };
    }
}
