using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Moq;
using PruebaENG.Application.Common.Interfaces;
using PruebaENG.Application.Users.Commands.CreateUser;
using PruebaENG.Domain.Constants;
using PruebaENG.Domain.Entities;
using Xunit;

namespace PruebaENG.Application.UnitTests.Users.Commands;

public class CreateUserRequestHandlerTests
{
    private readonly Mock<ILogger<CreateUserRequestHandler>> _logger;
    private readonly Mock<IApplicationDbContext> _dbContext;
    private readonly CreateUserRequestHandler _handler;
    
    public CreateUserRequestHandlerTests()
    {
        _logger = new Mock<ILogger<CreateUserRequestHandler>>();
        _dbContext = new Mock<IApplicationDbContext>();
        _handler = new CreateUserRequestHandler(_dbContext.Object, _logger.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateContract_WhenDataIsValid()
    {
        _logger.Setup(x => x.Log(
                It.IsAny<LogLevel>(), It.IsAny<EventId>(),
                It.IsAny<object>(), It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>()!))
            .Verifiable();
        
        _dbContext.Setup(x => x.Users.Add(It.IsAny<User>())).Returns(It.IsAny<EntityEntry<User>>);
        
        var newUser = new CreateUserRequest
        {
            Name = "Test",
            BirthDate = DateTime.Now
        };
        
        var result = await _handler.Handle(newUser, It.IsAny<CancellationToken>());
            
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(result.Messages, new []{Message.RegistrationSuccessful});
        _logger.Verify(x => x.Log(
            It.IsAny<LogLevel>(), It.IsAny<EventId>(),
            It.IsAny<object>(), It.IsAny<Exception>(),
            It.IsAny<Func<object, Exception, string>>()!), Times.Never);
    }

}