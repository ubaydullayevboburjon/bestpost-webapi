using Microsoft.AspNetCore.Http;

namespace BestPost.Service.Dtos.Posts;

public class PostCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = default!;
}
