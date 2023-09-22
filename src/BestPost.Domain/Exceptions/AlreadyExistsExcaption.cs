using System.Net;

namespace BestPost.Domain.Exceptions;
public class AlreadyExistsExcaption : ClientException
{
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.Conflict;

    public override string TitleMessage { get; protected set; } = String.Empty;
}
