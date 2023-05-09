using Application.Abstraction;

namespace Application.Services.Commands;

public record CreateTrainingCategoryRequest(string Name) : ICommand<bool>
{
}
