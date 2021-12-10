using PruebaENG.Domain.Common;

namespace PruebaENG.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}