using PruebaENG.Application.Common.Interfaces;

namespace PruebaENG.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}