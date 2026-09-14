using Microsoft.AspNetCore.Mvc;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public UsersController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

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
    
    
}