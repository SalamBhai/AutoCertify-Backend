using Application.Abstraction;

namespace Application.Services.Commands;

public sealed record UpdateRequest(string existingFullName, string FirstName, string LastName, string MiddleName) : ICommand<string>;
