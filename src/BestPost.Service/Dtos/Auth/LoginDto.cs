﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPost.Service.Dtos.Auth;

public class LoginDto
{
    public string Email { get; set; } = String.Empty;

    public string Password { get; set; } = string.Empty;
}
