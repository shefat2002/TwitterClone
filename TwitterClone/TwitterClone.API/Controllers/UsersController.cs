using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.API.Data;
using TwitterClone.API.Dtos;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/users
[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserRepository _userRepository;

    public UsersController(IConfiguration configuration, UserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    // GET: api/users
    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetUsers()
    {
        var users = _userRepository.GetUsers();
        return Ok(users.Select(user => new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        }));
    }
    
    // POST: api/users
    [HttpPost]
    [AllowAnonymous]
    public IActionResult CreateUser([FromBody] CeateUserDto userDto)
    {
        if(string.IsNullOrWhiteSpace(userDto.FirstName) || string.IsNullOrWhiteSpace(userDto.LastName) || string.IsNullOrWhiteSpace(userDto.Email))
        {
            return BadRequest("All fields are required.");
        }
        var existingUser = _userRepository.GetUserByEmail(userDto.Email);
        if (existingUser != null)
        {
            return BadRequest("User with this email already exists.");
        }
        var createdUser = _userRepository.AddUser(new User
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email
        });
        
        return Ok(new UserDto{
            Id = createdUser.Id,
            FirstName = createdUser.FirstName,
            LastName = createdUser.LastName,
            Email = createdUser.Email
        });
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public IActionResult GetUserById([FromRoute] Guid id)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        return Ok(new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        });
    }
    
    //PUT: api/users/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto userDto)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        
        var updatedUser = _userRepository.UpdateUser(user);
        
        return Ok(new UserDto
        {
            Id = updatedUser.Id,
            FirstName = updatedUser.FirstName,
            LastName = updatedUser.LastName,
            Email = updatedUser.Email
        });
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
        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var isDeleted = _userRepository.DeleteUser(user);

        return Ok(isDeleted);
    }

}