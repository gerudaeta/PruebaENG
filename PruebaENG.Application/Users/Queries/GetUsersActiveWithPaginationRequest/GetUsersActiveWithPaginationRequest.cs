using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaENG.Application.Common.Extensions;
using PruebaENG.Application.Common.Interfaces;
using PruebaENG.Application.Common.Wrapper;
using PruebaENG.Application.Dto;
using PruebaENG.Domain.Entities;

namespace PruebaENG.Application.Users.Queries.GetUsersActiveWithPaginationRequest;

public class GetUsersActiveWithPaginationRequest : IPaginacion, IRequest<PaginatedResult<UserDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetUsersWithPaginationRequestHandler : IRequestHandler<GetUsersActiveWithPaginationRequest, PaginatedResult<UserDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    
    public GetUsersWithPaginationRequestHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<UserDto>> Handle(GetUsersActiveWithPaginationRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<User, UserDto>> expression = e => new UserDto(e.Id, e.Name, e.BirthDate, e.Status);
        var queryable = _applicationDbContext.Users.Where(x => x.Status == true).AsNoTracking().AsQueryable();
            
        var users = await queryable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return _mapper.Map<PaginatedResult<UserDto>>(users);
    }
}