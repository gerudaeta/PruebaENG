using Microsoft.EntityFrameworkCore;
using PruebaENG.Application.Common.Interfaces;
using PruebaENG.Application.Common.Wrapper;
using PruebaENG.Domain.Constants;

namespace PruebaENG.Application.Users.Commands.DeleteUser;

public class DeleteUserRequest : IRequestWrapper<int>
{
    public int Id { get; set; }
}

public class DeleteUserRequestHandler : IRequestHandlerWrapper<DeleteUserRequest, int>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public DeleteUserRequestHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Response<int>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user is null)
        {
            return await Response<int>.FailAsync(Message.NotRegister);
        }

        _applicationDbContext.Users.Remove(user);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
            
        return await Response<int>.SuccessAsync(user.Id, Message.DeleteSuccessful);
    }
}