using System;
using System.ComponentModel.DataAnnotations;
using UserManagement.Domain.Models;

namespace UserManagement.Web.Models.Users;

public class UserCreateRequestModel
{
    public required string Forename { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required bool IsActive { get; set; }
    public required DateTime DateOfBirth { get; set; }
}
