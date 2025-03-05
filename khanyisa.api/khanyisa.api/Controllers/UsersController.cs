using Application.Interfaces.Service;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace khanyisa.api.Controllers;

[ApiController]
[Route(ApiEndpoints.Users.Base)]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet(ApiEndpoints.Users.Get)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] string userIdOrUsername, CancellationToken token)
    {
        UserResponse? user = await _userService.GetUserByUsernameAsync(userIdOrUsername, token);

        if (user is null)
        {
            return NotFound("USER NOT FOUND");
        }

        return Ok(user);
    }

    public async Task<IActionResult> CreateUser([FromBody] ApplicationUserRequest request, CancellationToken token)
    {
        UserResponse? response = await _userService.CreateUser(request, token);

        return Ok(response);
    }
}
