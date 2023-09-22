namespace BestPost.Service.Dtos.Auth;

public class VerfiyRegisterDto
{
    public string Email { get; set; } = string.Empty;

    public int Code { get; set; }
}
