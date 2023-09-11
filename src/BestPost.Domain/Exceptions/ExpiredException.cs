﻿using System.Net;

namespace BestPost.Domain.Exceptions;

public  class ExpiredException:ClientException
{
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.Gone;

    public override string TitleMessage { get; protected set; } = String.Empty;
}
