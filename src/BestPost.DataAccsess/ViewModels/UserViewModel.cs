using System.ComponentModel.DataAnnotations;

namespace BestPost.DataAccsess.ViewModels;

public class UserViewModel
{
    [MaxLength(100)]
    public string FirstName { get; set; } = String.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = String.Empty;

    public string Username { get; set; } = String.Empty;

    public string Email { get; set; } = string.Empty;

    public bool EmailConfirmed { get; set; }

    public string ImagePath { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
