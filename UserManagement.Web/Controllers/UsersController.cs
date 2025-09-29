using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List(bool? isActive = null)
    {
        // Get users based on the isActive filter
        var users = isActive.HasValue
            ? _userService.FilterByActive(isActive.Value)
            : _userService.GetAll();

        // Map to view models
        var userViewModels = users.Select(user => new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth
        }).ToList();

        // Create the list view model
        var model = new UserListViewModel
        {
            Items = userViewModels
        };

        return View(model);
    }
}
