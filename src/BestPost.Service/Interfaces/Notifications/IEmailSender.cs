using BestPost.Service.Dtos.Notifications;

namespace BestPost.Service.Interfaces.Notifications;

public interface IEmailSender
{
    public Task<bool> SendAsync(EmailMessage emailMessage);

}
