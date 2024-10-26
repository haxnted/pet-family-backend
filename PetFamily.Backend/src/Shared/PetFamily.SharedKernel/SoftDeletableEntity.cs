namespace PetFamily.SharedKernel;

public abstract class SoftDeletableEntity<TId> : Entity<TId> where TId : notnull
{
    protected SoftDeletableEntity(TId id) : base(id) { }

    protected bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    
    public virtual void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }

    public virtual void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
    
}
