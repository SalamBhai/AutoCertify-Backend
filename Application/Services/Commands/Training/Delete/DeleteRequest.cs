using Application.Abstraction;

namespace Application.Services.Commands;

public record DeleteTrainingRequest(string trainingName) : ICommand<string>;

