using Application.Abstraction;
using Application.DTOs;

namespace Application.Services.Commands;

public record CreateParticipantRequest(string FirstName, string LastName, string MiddleName, Guid TrainingId) : ICommand<bool>
{
}
