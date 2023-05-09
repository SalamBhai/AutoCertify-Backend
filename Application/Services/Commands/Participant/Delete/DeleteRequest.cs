using Application.Abstraction;

namespace Application.Services.Commands;

public record DeleteRequest(string fullName) : ICommand<string>;

