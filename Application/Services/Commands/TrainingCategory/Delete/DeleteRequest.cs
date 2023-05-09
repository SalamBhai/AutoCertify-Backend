using Application.Abstraction;

namespace Application.Services.Commands;

public record DeleteTrainingCategoryRequest(string trainingCategoryName) : ICommand<string>;

