using System;
using UserManagement.Domain.Models;

namespace UserManagement.Web.Models.Users;

public class UserEditRequestModel : UserCreateRequestModel
{
    public required long Id { get; set; }
}
