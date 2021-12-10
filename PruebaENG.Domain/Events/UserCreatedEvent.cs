using PruebaENG.Domain.Common;
using PruebaENG.Domain.Entities;

namespace PruebaENG.Domain.Events;

public class UsuarioCreadoEvent : DomainEvent
{
    public UsuarioCreadoEvent(User user)
    {
        User = user;
    }

    public User User { get; }
}