namespace BestPost.Domain.Entites.Posts;

public class Post : Auditable
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;

    public long UserId { get; set; }
}
