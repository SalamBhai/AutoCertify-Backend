using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.Wrapper;

namespace Application.Services.Commands;

public record DeleteTrainingHandler : ICommandHandler<DeleteTrainingRequest, string>
{
    private readonly ITrainingRepository _trainingRepository;

    public DeleteTrainingHandler(ITrainingRepository trainingRepository)
    => (_trainingRepository) = (trainingRepository);
    
    public async Task<Result<string>> Handle(DeleteTrainingRequest request, CancellationToken cancellationToken)
    {
        var training = await _trainingRepository.GetTrainingAsync(pt => pt.TrainingName == request.trainingName && pt.IsDeleted == false, false);
        if (training is null) return new  Result<string>{
            Messages = new List<string> {
                $"Training With Name: {request.trainingName} could not be found"},
                Succeeded = false,

        };
        var trainingReturned =  training.Delete();
        var savedResponse = await _trainingRepository.UpdateAsync(training);
        return new  Result<string>{
            Messages = new List<string> {
                $"Delete Request For Training: {request.trainingName} was successful"},
                Succeeded = true,

        };
    }
}
