namespace PruebaENG.Domain.Common;

public abstract class AuditableEntity
{
    public DateTime CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }
}