using Microsoft.AspNetCore.Http;

namespace BestPost.Service.Dtos.Users;

public class UploadImageDto
{
    public IFormFile Image { get; set; } = default!;
}
