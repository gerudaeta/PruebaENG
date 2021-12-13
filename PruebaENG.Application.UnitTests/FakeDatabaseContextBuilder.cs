using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using PruebaENG.Infrastructure.Persistence;
using PruebaENG.Infrastructure.Services;

namespace PruebaENG.Application.UnitTests;

internal class FakeDatabaseContextBuilder
{
    public ApplicationDbContext Build([CallerMemberName] string contextName = null)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase(contextName);
        optionsBuilder.EnableSensitiveDataLogging();
        return new ApplicationDbContext(optionsBuilder.Options, new DomainEventService(null, null), new DateTimeService());
    }
}