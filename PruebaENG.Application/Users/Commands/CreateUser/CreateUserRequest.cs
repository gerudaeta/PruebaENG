using Microsoft.Extensions.Logging;
using PruebaENG.Application.Common.Interfaces;
using PruebaENG.Application.Common.Wrapper;
using PruebaENG.Domain.Constants;
using PruebaENG.Domain.Entities;

namespace PruebaENG.Application.Users.Commands.CreateUser;

public class CreateUserRequest : IRequestWrapper<int>
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
}

public class CreateUserRequestHandler : IRequestHandlerWrapper<CreateUserRequest, int>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ILogger<CreateUserRequestHandler> _logger;

    public CreateUserRequestHandler(IApplicationDbContext applicationDbContext, ILogger<CreateUserRequestHandler> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    public async Task<Response<int>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = request.Name,
            BirthDate = request.BirthDate
        };

        await _applicationDbContext.Users.AddAsync(user, cancellationToken);

        try
        {
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return await Response<int>.SuccessAsync(user.Id, Message.RegistrationSuccessful);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"Error saving user: {e.Message} - {e.StackTrace}");
            return await Response<int>.FailAsync(Message.RegistrationError);
        }
        
    }
}