namespace BestPost.Domain.Entites.Likes;

public class Like:Auditable
{
    public long PostId { get; set; }
    public long UserId { get; set; }
    public Boolean IsLiked { get; set; }
}
