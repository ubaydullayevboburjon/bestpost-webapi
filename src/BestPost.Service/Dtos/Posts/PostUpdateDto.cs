using Microsoft.AspNetCore.Http;

namespace BestPost.Service.Dtos.Posts;

public class PostUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile ImagePath { get; set; } = default!;
}
