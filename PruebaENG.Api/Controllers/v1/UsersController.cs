using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaENG.Application.Common.Wrapper;
using PruebaENG.Application.Dto;
using PruebaENG.Application.Users.Commands.ChangeStatusUser;
using PruebaENG.Application.Users.Commands.CreateUser;
using PruebaENG.Application.Users.Queries.GetUsersActiveWithPaginationRequest;

namespace PruebaENG.Api.Controllers.v1;

[ApiVersion("1.0")]
public class UsersController : ApiControllerBase
{
    [HttpPost("CreateUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<int>))]
    public async Task<ActionResult<Response<int>>> CreateAgenda(CreateUserRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
    
    [HttpPut("ChangeStatusUser/{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<int>))]
    public async Task<ActionResult> ChangeStatusUser(int id, ChangeStatusUserRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(request);

        return NoContent();
    }
    
    [HttpGet("GetUsersActiveWithPagination")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResult<UserDto>))]
    public async Task<ActionResult<PaginatedResult<UserDto>>> GetUsersActiveWithPagination([FromQuery] GetUsersActiveWithPaginationRequest request)
    {
        return await Mediator.Send(request);
    }
}