namespace BestPost.Domain.Exceptions.Files;

public class ImageNotFoundException : NotFoundException
{
    public ImageNotFoundException()
    {
        TitleMessage = "Image not found !";
    }
}
