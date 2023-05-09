
using Application.DTOs;
using Application.Exceptions;
using Application.Abstraction.Repositories;
using Application.Wrapper;
using Domain.Entities;
using Application.Abstraction;

namespace Application.Services.Commands;

public record CreateTrainingCategoryRequestHandler : ICommandHandler<CreateTrainingCategoryRequest, bool>
{
    private readonly ITrainingCategoryRepository _trainingCategoryRepository;

    public CreateTrainingCategoryRequestHandler(ITrainingCategoryRepository trainingCategoryRepository)
    => (_trainingCategoryRepository) = (trainingCategoryRepository);
    

    public async Task<Result<bool>> Handle(CreateTrainingCategoryRequest request, CancellationToken cancellationToken)
    {
        var TrainingCategoryExists = await _trainingCategoryRepository.ExistsAsync(p => p.Name == request.Name);
        if(TrainingCategoryExists) 
        {
            return new Result<bool>
            {
                Messages = new List<string> {
                $"A Record With The Name: {request.Name} already exists"},
                Succeeded = false,
            
            };
        }
        
        var trainingCategory = new Domain.Entities.TrainingCategory(false, request.Name);
        var saveResponse = (await _trainingCategoryRepository.CreateAsync(trainingCategory));

        if(saveResponse.Name != request.Name)
        return new Result<bool>
        {
            Messages = new List<string> {
                $"Registration request for TrainingCategory failed."},
            Succeeded = false,
            
        };
        return new Result<bool>
        {
            Messages = new List<string> {
                $"Registration request for TrainingCategory: {saveResponse.Name} was successful",
                $"Here is the id: {saveResponse.Id}",
            },
            Succeeded = true,
            
        };
    }
}
