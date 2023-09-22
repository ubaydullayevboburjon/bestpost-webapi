namespace BestPost.Domain.Exceptions.Posts;

public class PostNotFoundException : Exception
{
    private string TitleMessage;
    public PostNotFoundException()
    {
        TitleMessage = "Post not found!";
    }
}
