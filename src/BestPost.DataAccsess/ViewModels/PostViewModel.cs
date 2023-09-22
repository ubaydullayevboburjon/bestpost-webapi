namespace BestPost.DataAccsess.ViewModels;

public class PostViewModel
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;

    public long UserId { get; set; }

    public DateTime CreatedAT { get; set; }
    public DateTime UpdatedAT { get; set; }

}
