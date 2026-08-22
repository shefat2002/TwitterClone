namespace TwitterClone.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    
    public BaseEntity(Guid id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }
    public virtual string DescribeRecord()
    {
        return $"From Base Class! Id: {Id}, CreatedAt: {CreatedAt},ModifiedAt: {ModifiedAt}, CreatedBy: {CreatedBy},ModifiedBy: {ModifiedBy}";
    }
}