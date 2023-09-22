namespace BestPost.Domain.Entites.Posts;

public class Like : Auditable
{
    public long PostId { get; set; }
    public long UserId { get; set; }
    public bool IsLiked { get; set; }
}
