using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPost.Service.Interfaces.Auth;

public interface IIdentityService
{
    public long UserId { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string Email { get; }

}
