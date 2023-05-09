using Domain.DomainException;

namespace Domain.Entities;

public class Training 
{
    public Training(string trainingName, DateTime dateOfCertificateIssuance, Guid trainingCategoryId)
    {
         TrainingName = trainingName;
         DateOfCertificateIssuance = dateOfCertificateIssuance;
         
         TrainingCategoryId = trainingCategoryId;
         if(TrainingName is null)
        {
            throw new ArgumentCannotBeNullException("Empty Value Cannot Be Accepted For The Field: TrainingName");
        }    
        
    }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public bool IsDeleted { get; private set; }
    public DateTime DeletedOn { get; private set; }
    public string TrainingName { get; private set; }
    public DateTime DateOfCertificateIssuance { get; private set; }
    public TrainingCategory? TrainingCategory { get; private set; }
    public Guid TrainingCategoryId { get; private set; }
    public ICollection<Participant> Participants{get; private set;} = new HashSet<Participant>();

    public Training Update(DateTime dateOfCertificateIssuance, string trainingName)
    {
        this.DateOfCertificateIssuance = dateOfCertificateIssuance;
        this.TrainingName = trainingName;
        return this;
    }
   
   public Training Delete()
   {
        this.IsDeleted = true;
        this.DeletedOn = DateTime.UtcNow;
        return this;
   }
}
