using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPost.Domain.Entites.Users;

public class User:Auditable
{
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = String.Empty;

    public string Username { get; set; } = String.Empty;

    public string Email { get; set; } = string.Empty;

    public bool EmailConfirmed { get; set; }

    public string ImagePath { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = String.Empty;

    public string PasswordSalt { get; set; } = String.Empty;


}
