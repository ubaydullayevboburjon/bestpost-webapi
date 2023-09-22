namespace BestPost.Domain.Exceptions.Users;

public class UsernameAlreadyExcaptions : AlreadyExistsExcaption
{
    public UsernameAlreadyExcaptions()
    {
        TitleMessage = "User already exists";
    }
    public UsernameAlreadyExcaptions(string username)
    {
        TitleMessage = "This username is already registered";
    }
}
