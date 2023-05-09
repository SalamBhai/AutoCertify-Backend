using Domain.Entities;

namespace Application.DTOs;

public class ParticipantDto
{
    public Guid Id { get; set; } 
    public string FirstName { get;  set; }
    public string LastName { get;  set; }
    public string FullName { get; set; }
    public string MiddleName { get;  set; }
    public string CertificateNumber { get;  set; }
    public Guid TrainingId { get;  set; }
    public TrainingDto Training { get; set; }
    public TrainingCategoryDto TrainingCategory {get; set;}
}

public class TrainingDto
{
    public string TrainingName {get; set;}
    public string TrainingCategoryName {get; set;}
    public DateTime DateOfCertificateIssuance {get; set;}
    public Guid Id {get; set;}
}
public class TrainingCategoryDto
{
    public Guid Id {get; set;}
    public string Name {get; set;}
}
