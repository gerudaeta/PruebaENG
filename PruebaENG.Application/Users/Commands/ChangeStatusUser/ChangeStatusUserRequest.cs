using Microsoft.EntityFrameworkCore;
using PruebaENG.Application.Common.Interfaces;
using PruebaENG.Application.Common.Wrapper;
using PruebaENG.Domain.Constants;

namespace PruebaENG.Application.Users.Commands.ChangeStatusUser;

public class ChangeStatusUserRequest : IRequestWrapper<int>
{
    public int Id { get; set; }
}

public class ChangeStatusUserRequestHandler : IRequestHandlerWrapper<ChangeStatusUserRequest, int>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public ChangeStatusUserRequestHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Response<int>> Handle(ChangeStatusUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
        if (user is null)
        {
            return await Response<int>.FailAsync(Message.NotRegister);
        }

        user.Status = !user.Status;
            
        _applicationDbContext.Users.Update(user);
        
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
            
        return await Response<int>.SuccessAsync(user.Id, Message.ChangeStatusSuccessful);
    }
}