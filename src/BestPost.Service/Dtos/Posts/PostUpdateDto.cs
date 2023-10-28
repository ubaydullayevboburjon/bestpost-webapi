namespace BestPost.Service.Dtos.Posts;

public class PostUpdateDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
