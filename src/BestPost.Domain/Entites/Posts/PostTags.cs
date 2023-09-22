namespace BestPost.Domain.Entites.Posts;

public class PostTags : Auditable
{
    public long PostId { get; set; }

    public string Tags { get; set; } = string.Empty;
}
