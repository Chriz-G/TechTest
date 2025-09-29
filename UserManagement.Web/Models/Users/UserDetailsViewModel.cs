using System;
using UserManagement.Domain.Models;

namespace UserManagement.Web.Models.Users;

public class UserDetailsViewModel
{
    public long Id { get; set; }
    public string? Forename { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateOfBirth { get; set; }
}
