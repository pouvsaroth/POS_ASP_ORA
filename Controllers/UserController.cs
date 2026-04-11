using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult ViewUser()
    {
        var users = _userService.GetUsers();
        return View("~/Views/Settings/User.cshtml", users);
    }

    [HttpPost]
    public IActionResult Create(Users model)
    {
        _userService.InsertUser(model);
        return RedirectToAction("ViewUser");
    }

    [HttpPost]
    public IActionResult Update(Users model)
    {
        _userService.UpdateUser(model);
        return RedirectToAction("ViewUser");
    }

    [HttpPost]
    public IActionResult DeleteSelected([FromBody] List<Guid> ids)
    {
        foreach (var id in ids)
        {
            _userService.DeleteUser(id);
        }

        return Ok(new { message = "Users deleted successfully" });
    }
}