using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/users
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public UsersController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // GET: api/users
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = new List<User>
        {
            new() { FirstName = "Rahim", LastName = "Uddin", Email = "rahimuddin@example.com" },
            new() { FirstName = "Karim", LastName = "Uddin", Email = "karimuddin@example.com" },
            new() { FirstName = "Jamal", LastName = "Uddin", Email = "jamaluddin@example.com" }
        };
        return Ok(users);
    }
    
    // POST: api/users
    [HttpPost]
    [AllowAnonymous]
    public IActionResult CreateUser()
    {
        return Ok(new
        {
            UserId = Guid.NewGuid(),
            FirstName = "Kamal",
            LastName = "Uddin",
            Email = "kamaluddin@example.com"
        });
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public IActionResult GetUserById([FromRoute] Guid id)
    {
        return Ok(new
        {
            UserId = id,
            FirstName = "Jamal",
            LastName = "Uddin",
            Email = "kamaluddin@example.com"
        });
    }
    
    //PUT: api/users/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateUser([FromRoute] Guid id)
    {
        return Ok(new
        {
            UserId = id,
            FirstName = "Kamal",
            LastName = "Pasha",
            Email = "kamaluddin@example.com"
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
        return Ok(new
        {
            UserId = id,
            Message = "User deleted successfully."
        });
    }

}