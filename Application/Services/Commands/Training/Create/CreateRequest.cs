using Application.Abstraction;

namespace Application.Services.Commands;

public record CreateRequest(string Name, DateTime dateOfCertificateIssuance, Guid trainingCategoryId) : ICommand<bool>
{
}
