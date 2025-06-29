namespace STOX.Data.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }

    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    public override bool Equals(object? obj)
    {
        if(ReferenceEquals(this, obj))
            return true;
        if(obj is null || GetType() != obj.GetType())
            return false;
        return Id.Equals(((BaseEntity)obj).Id);
    }
    
    public override int GetHashCode() => Id.GetHashCode();
}