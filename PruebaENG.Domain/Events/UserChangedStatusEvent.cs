using PruebaENG.Domain.Entities;

namespace PruebaENG.Domain.Events;

public class UserChangedStatusEvent
{
    public UserChangedStatusEvent(User user)
    {
        User = user;
    }

    public User User { get; }
}