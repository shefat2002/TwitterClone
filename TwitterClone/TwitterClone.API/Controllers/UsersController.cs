using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.API.Data;
using TwitterClone.Application.Dtos.User;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/users
[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/users
    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetUsers()
    {
        return Ok(_userService.GetUsers());
    }
    
    // POST: api/users
    [HttpPost]
    [AllowAnonymous]
    public IActionResult CreateUser([FromBody] CeateUserDto userDto)
    {
        var createdUser = _userService.CreateUserDto(userDto);
        if(createdUser == null)
        {
            return BadRequest("Invalid user data or email already exists.");
        }
        
        return Ok(createdUser);
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public IActionResult GetUserById([FromRoute] Guid id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        return Ok(user);
    }
    
    //PUT: api/users/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto userDto)
    {
        var user = _userService.UpdateUser(id, userDto);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        return Ok(user);
    }
    
    // PATCH: api/users/{id}/firstname
    [HttpPatch("{id}/firstname")]
    public IActionResult PatchUser([FromRoute] Guid id, [FromBody] string firstName)
    {
        return Ok(new
        {
            UserId = id,
            FirstName = "Joshim",
        });
    }
    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        var isDeleted = _userService.DeleteUser(id);
        if (isDeleted == false)
        {
            return NotFound("User not found.");
        }
        return Ok(isDeleted);
    }

}