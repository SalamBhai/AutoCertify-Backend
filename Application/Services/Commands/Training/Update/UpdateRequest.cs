using Application.Abstraction;
using Application.DTOs;

namespace Application.Services.Commands;

public sealed record UpdateTrainingRequest(DateTime dateOfCertificateIssuance, string existingTrainingName, string trainingName) : ICommand<string>;
