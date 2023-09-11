namespace BestPost.Domain.Exceptions.Users;

public class UserAlreadyExistsExcaption : AlreadyExistsExcaption
{
    public UserAlreadyExistsExcaption()
    {
        TitleMessage = "User already exists";
    }

    public UserAlreadyExistsExcaption(string email)
    {
        TitleMessage = "This email is already registered";
    }

}
