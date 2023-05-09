using Domain.DomainException;

namespace Domain.Entities;

public class TrainingCategory
{
    public TrainingCategory(bool isDeleted, string name)
    {
        IsDeleted = isDeleted;
        Name = name;
        Trainings = new HashSet<Training>();
        if(Name is null)
        {
            throw new ArgumentCannotBeNullException("Empty Value Cannot Be Accepted For The Field: Name");
        }   
        
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public bool IsDeleted { get; private set; }
    public DateTime DeletedOn { get; private set; }
    public string Name {get; private set;}  
    public ICollection<Training> Trainings {get; private set;}
    public TrainingCategory Update(string name)
    {
        this.Name = name; 
        return this;
    }
    public TrainingCategory Delete()
    {
        this.IsDeleted = true;
        this.DeletedOn = DateTime.UtcNow;
        return this;
    }
}
