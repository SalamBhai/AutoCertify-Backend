using Application.Abstraction;
using Application.DTOs;

namespace Application.Services.Commands;

public sealed record UpdateTrainingCategoryRequest(string name, string existingName) : ICommand<string>;
