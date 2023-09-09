using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPost.Service.Dtos.Users;

public class UserUpdateDto
{
    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public string PhoneNumber { get; set; } = String.Empty;
}
