using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaENG.Application.Common.Wrapper;
using PruebaENG.Application.Users.Commands.ChangeStatusUser;
using PruebaENG.Application.Users.Commands.CreateUser;

namespace PruebaENG.Api.Controllers.v1;

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
}