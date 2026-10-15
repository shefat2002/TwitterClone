namespace TwitterClone.Application.Dtos.Follow;

public class CreateFollowDto
{
    public required Guid FollowerId { get; set; }
    public required Guid FollowingId { get; set; }
}