namespace TwitterClone.Domain.Entities;

public class Follow : BaseEntity
{
    public Guid FollowerId { get; set; }
    public Guid FollowingId { get; set; }

    public Follow() : base(Guid.NewGuid())
    {
    }
    public override string DescribeRecord()
    {
        var baseRecord = base.DescribeRecord();
        return $"{baseRecord}, FollowerId: {FollowerId}, FollowingId: {FollowingId}";
    }
}