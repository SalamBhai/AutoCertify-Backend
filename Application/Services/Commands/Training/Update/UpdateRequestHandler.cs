using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.Wrapper;

namespace Application.Services.Commands;

public record UpdateTrainingRequestHandler : ICommandHandler<UpdateTrainingRequest, string>
{
    private readonly ITrainingRepository _trainingRepository;

    public UpdateTrainingRequestHandler(ITrainingRepository trainingRepository) =>
    (_trainingRepository) = (trainingRepository);

    public async Task<Result<string>> Handle(UpdateTrainingRequest request, CancellationToken cancellationToken)
    {
        var training =  await _trainingRepository.GetTrainingAsync(pt => pt.TrainingName == request.existingTrainingName
        ,false);
        if (training is null) return new Result<string>
            {
                Messages = new List<string> {
                $"Training with {request.existingTrainingName} could not be found"},
                Succeeded = false,

            };
        var updatedTraining = training.Update(request.dateOfCertificateIssuance, request.trainingName);
        var savedResponse = await _trainingRepository.UpdateAsync(updatedTraining);
        return new  Result<string>{
            Messages = new List<string> {
                $"Update Request For Training: {request.trainingName} was successful"},
                Succeeded = true,

        };

    }
}
