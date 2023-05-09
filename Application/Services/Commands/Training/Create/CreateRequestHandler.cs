
using Application.DTOs;
using Application.Exceptions;
using Application.Abstraction.Repositories;
using Application.Wrapper;
using Domain.Entities;
using Application.Abstraction;

namespace Application.Services.Commands;

public record CreateRequestHandler : ICommandHandler<CreateRequest, bool>
{
    private readonly ITrainingRepository _trainingRepository;

    public CreateRequestHandler(ITrainingRepository trainingRepository)
    => (_trainingRepository) = (trainingRepository);
    

    public async Task<Result<bool>> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        var trainingExists = await _trainingRepository.ExistsAsync(p => p.TrainingName == request.Name);
        if(trainingExists) 
        {
             return new Result<bool>
            {
                Messages = new List<string> {
                $"A Record With The Name: {request.Name} already exists"},
                Succeeded = false,

            };
        }
        
        var training = new Domain.Entities.Training(request.Name, 
        new DateTime(), request.trainingCategoryId);
        var saveResponse = (await _trainingRepository.CreateAsync(training));

        if(saveResponse.TrainingName != request.Name)
        return new Result<bool>
        {
            Messages = new List<string> {
                $"Registration request for training failed."},
            Succeeded = false,
            
        };
        return new Result<bool>
        {
            Messages = new List<string> {
                $"Registration request for training: {saveResponse.TrainingName} was successful",
                $"Here is the id: {saveResponse.Id}"},

            Succeeded = true,
            
        };
    }
}
