namespace TwitterClone.Application.Dtos.User;

public class CeateUserDto
{
    public required string FirstName { get; set; } = null!;
    public required string LastName { get; set; } = null!;
    public required string Email { get; set; } = null!;
}