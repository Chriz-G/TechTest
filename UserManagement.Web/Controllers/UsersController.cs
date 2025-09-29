using System.Linq;
using System.Threading.Tasks;
using UserManagement.Domain.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IMappingService _mapper;
    public UsersController(IUserService userService, IMappingService mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("")]
    public ViewResult List(bool? isActive = null)
    {
        // Get users based on the isActive filter
        var users = isActive.HasValue
            ? _userService.FilterByActive(isActive.Value)
            : _userService.GetAll();

        // Map to view models
        var userViewModels = users
            .Select(user => _mapper.Map<UserModel, UserListItemViewModel>(user))
            .ToList();

        // Create the list view model
        var result = new UserListViewModel
        {
            Items = userViewModels
        };

        return View(result);
    }

    [HttpGet]
    [Route("details/{id}")]
    public async Task<IActionResult> Details(long id)
    {
        var result = await _userService.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        var viewModel = _mapper.Map<UserModel, UserDetailsViewModel>(result);
        return View(viewModel);
    }

    [HttpGet]
    [Route("create")]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("create")]
    public async Task<IActionResult> Create(UserCreateRequestModel requestModel)
    {
        if (!ModelState.IsValid)
        {
            return View(requestModel);
        }
        var user = _mapper.Map<UserCreateRequestModel, UserModel>(requestModel);
        var result = await _userService.CreateAsync(user);
        var viewModel = _mapper.Map<UserModel, UserDetailsViewModel>(result);
        return View("Details", viewModel);
    }

    [HttpGet]
    [Route("edit/{id}")]
    public async Task<IActionResult> Edit(long id)
    {
        var result = await _userService.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        var viewModel = _mapper.Map<UserModel, UserEditRequestModel>(result);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("edit/{id}")]
    public async Task<IActionResult> Edit(long id, UserEditRequestModel requestModel)
    {
        if (!ModelState.IsValid)
        {
            return View(requestModel);
        }
        var user = _mapper.Map<UserEditRequestModel, UserModel>(requestModel);
        var result = await _userService.UpdateAsync(user);
        var viewModel = _mapper.Map<UserModel, UserDetailsViewModel>(result);
        return View("Details", viewModel);
    }

    [HttpGet]
    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _userService.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        var viewModel = _mapper.Map<UserModel, UserDetailsViewModel>(result);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("delete/{id}")]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var success = await _userService.DeleteAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(List));
    }
}
