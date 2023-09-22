using BestPost.Domain.Exceptions;

namespace BestPost.Domain.Exceptionsl;

public class ImageNotFoundExceptions : NotFoundException
{
    public ImageNotFoundExceptions()
    {
        this.TitleMessage = "Image not found!";
    }
}
