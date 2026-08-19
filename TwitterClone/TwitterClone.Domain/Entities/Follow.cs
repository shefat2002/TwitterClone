namespace TwitterClone.Domain.Entities;

public class Follow : BaseEntity
{
    public Guid FollowerId { get; set; }
    public Guid FollowingId { get; set; }

    public Follow(Guid followerId, Guid followingId) : base(Guid.NewGuid())
    {
        FollowerId = followerId;
        FollowingId = followingId;
    }
}