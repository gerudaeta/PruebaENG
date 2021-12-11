using PruebaENG.Domain.Common;

namespace PruebaENG.Domain.Entities;

public class User : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public bool Status { get; set; } = true;
    public List<DomainEvent> DomainEvents { get; set; }
}