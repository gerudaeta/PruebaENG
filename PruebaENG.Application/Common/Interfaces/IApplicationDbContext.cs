using Microsoft.EntityFrameworkCore;
using PruebaENG.Domain.Entities;

namespace PruebaENG.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
}