using System;
using System.Linq;
using UserManagement.Domain.Models;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;
using UserManagement.WebMS.Controllers;

namespace UserManagement.Data.Tests;

public class UserControllerTests
{

    [Fact]
    public void List_WhenServiceReturnsActiveUsers_ModelMustOnlyContainActiveUsers()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        var users = SetupUsers();
        var activeUsers = users.Where(x => x.IsActive).ToArray();

        // Act: Invokes the method under test with the arranged parameters.
        var result = controller.List(true);

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Model
            .Should().BeOfType<UserListViewModel>()
            .Which.Items.Should().BeEquivalentTo(activeUsers);
    }

    [Fact]
    public void List_WhenServiceReturnsInactiveUsers_ModelMustOnlyContainInactiveUsers()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        var users = SetupUsers();
        var activeUsers = users.Where(x => !x.IsActive).ToArray();

        // Act: Invokes the method under test with the arranged parameters.
        var result = controller.List(false);

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Model
            .Should().BeOfType<UserListViewModel>()
            .Which.Items.Should().BeEquivalentTo(activeUsers);
    }

    [Fact]
    public void List_WhenServiceReturnsUsers_ModelMustContainUsers()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        var users = SetupUsers();

        // Act: Invokes the method under test with the arranged parameters.
        var result = controller.List();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Model
            .Should().BeOfType<UserListViewModel>()
            .Which.Items.Should().BeEquivalentTo(users);
    }

    private UserModel[] SetupUsers()
    {
        var users = new[]
        {
            new UserModel { Id = 1, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1990-01-01") },
            new UserModel { Id = 2, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1991-07-22") },
            new UserModel { Id = 3, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", IsActive = false, DateOfBirth = DateTime.Parse("1992-06-12") },
            new UserModel { Id = 4, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1993-11-27") },
            new UserModel { Id = 5, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1994-08-01") },
            new UserModel { Id = 6, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1995-12-25") },
            new UserModel { Id = 7, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", IsActive = false, DateOfBirth = DateTime.Parse("1970-09-18") },
            new UserModel { Id = 8, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", IsActive = false, DateOfBirth = DateTime.Parse("1990-06-23") },
            new UserModel { Id = 9, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", IsActive = false, DateOfBirth = DateTime.Parse("1990-08-01") },
            new UserModel { Id = 10, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1990-10-01") },
            new UserModel { Id = 11, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1997-11-01") },
        };

        _userService
            .Setup(s => s.GetAll())
            .Returns(users);

        _userService
            .Setup(s => s.FilterByActive(false))
            .Returns(users.Where(x => !x.IsActive).ToArray());

        _userService
            .Setup(s => s.FilterByActive(true))
            .Returns(users.Where(x => x.IsActive).ToArray());

        return users;
    }

    private readonly Mock<IUserService> _userService = new();
    private readonly Mock<IMappingService> _mappingService = new();
    private UsersController CreateController()
    {
        SetupMapper();
        return new UsersController(_userService.Object, _mappingService.Object);
    }

    private void SetupMapper()
    {
        _mappingService.Setup(x => x.Map<User, UserModel>(It.IsAny<User>()))
            .Returns(new Func<User, UserModel>(user => new UserModel
            {
                Id = user.Id,
                Forename = user.Forename,
                Surname = user.Surname,
                Email = user.Email,
                IsActive = user.IsActive,
                DateOfBirth = user.DateOfBirth
            }));

        _mappingService.Setup(x => x.Map<UserModel, UserListItemViewModel>(It.IsAny<UserModel>()))
            .Returns(new Func<UserModel, UserListItemViewModel>(user => new UserListItemViewModel
            {
                Id = user.Id,
                Forename = user.Forename,
                Surname = user.Surname,
                Email = user.Email,
                IsActive = user.IsActive,
                DateOfBirth = user.DateOfBirth
            }));
    }
}
