namespace BestPost.Domain.Entites.Posts;

public class Coment : Auditable
{
    public long PostId { get; set; }
    public long UserId { get; set; }
    public long ReplayComentId { get; set; }

    public string ComentText { get; set; } = string.Empty;
}
