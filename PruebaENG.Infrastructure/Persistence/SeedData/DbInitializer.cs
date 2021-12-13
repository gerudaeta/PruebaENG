using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaENG.Application.Common.Interfaces;
using PruebaENG.Domain.Entities;

namespace PruebaENG.Infrastructure.Persistence.SeedData;

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IDateTime _dateTime;
    private readonly ILogger<DbInitializer> _logger;

    public DbInitializer(ApplicationDbContext applicationDbContext, IDateTime dateTime, ILogger<DbInitializer> logger)
    {
        _applicationDbContext = applicationDbContext;
        _dateTime = dateTime;
        _logger = logger;
    }

    public async Task Initialize()
    {
        try
        {
            _logger.LogInformation("Creando base de datos y aplicando migraciones");
            await _applicationDbContext.Database.EnsureCreatedAsync();

            if (!await _applicationDbContext.Users.AnyAsync())
            {
                _logger.LogInformation("Agregando usuarios");
                await _applicationDbContext.Users.AddRangeAsync(new List<User>
                {
                    new() { Name = "German 1", BirthDate = _dateTime.Now },
                    new() { Name = "German 2", BirthDate = _dateTime.Now },
                    new() { Name = "German 3", BirthDate = _dateTime.Now },
                    new() { Name = "German 4", BirthDate = _dateTime.Now },
                    new() { Name = "German 5", BirthDate = _dateTime.Now, Status = false },
                    new() { Name = "German 6", BirthDate = _dateTime.Now, Status = false }
                });
            
                _logger.LogInformation("Usuarios agregados");
                await _applicationDbContext.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            _logger.LogCritical($"Error al crear DB Seed: {e.Message} - {e.StackTrace}");
        }
        
    }
}