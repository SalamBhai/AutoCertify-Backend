using Domain.DomainException;

namespace Domain.Entities;

public class Participant
{
    public Participant(bool isDeleted, string fullName, string firstName, string lastName, string middleName, string certificateNumber, Guid trainingId)
    {
       
        IsDeleted = isDeleted;
        FirstName = firstName;
        LastName = lastName;
        FullName = fullName;
        MiddleName = middleName;
        CertificateNumber = certificateNumber;
        TrainingId = trainingId;
        if(FirstName is null)
        {
            throw new ArgumentCannotBeNullException("Empty Value Cannot Be Accepted For The Field: FirstName");
        }   
        if(LastName is null)
        {
            throw new ArgumentCannotBeNullException("Empty Value Cannot Be Accepted For The Field: LastName");
        }
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public bool IsDeleted { get; private set; }
    public DateTime DeletedOn { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName { get; private set; }
    public string MiddleName { get; private set; }
    public string CertificateNumber { get; private set; }
    public Guid TrainingId { get; private set; }
    public Training Training { get; private set; }

    public Participant Update(string lastName, string middleName, string firstName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.MiddleName = middleName;
        this.FullName = $"{firstName} {lastName}";
        return this;
    }
    public Participant Delete()
    {
        this.IsDeleted = true;
        this.DeletedOn = DateTime.UtcNow;
        return this;
    }

}
