using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPost.Service.Dtos.Auth;

public class VerfiyRegisterDto
{
    public string Email { get; set; } = string.Empty;

    public long Code { get; set; }
}
