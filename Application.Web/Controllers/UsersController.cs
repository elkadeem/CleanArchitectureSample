using Application.Core;
using Application.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using X.PagedList;

namespace Application.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsersService _usersService;
        private readonly ILogger<UsersController> _logger;
        private const int PageSize = 1;
        public UsersController(UsersService usersService
            , ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }
        // GET: UsersController
        public async Task<ActionResult> Index(UsersListViewModel viewModel)
        {
            viewModel.Page = viewModel.Page <= 0 ? 1 : viewModel.Page;
            var result = await _usersService.GetAsync(viewModel.Username
                , viewModel.Name, viewModel.Email
                , null
                , viewModel.Page
                , PageSize);

            viewModel.Users = new StaticPagedList<User>(result.Items, viewModel.Page
                , PageSize, result.TotalItemsCount);
            return View(viewModel);
        }

        // GET: UsersController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var user = await _usersService.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            UserViewModel model = new UserViewModel();
            FillLookups(model);

            return View(model);
        }

        private static void FillLookups(UserViewModel model)
        {
            model.Countries = new List<SelectListItem> {
                new SelectListItem { Text = "USA", Value = "USA" },
                new SelectListItem { Text = "Canada", Value = "Canada" },
                new SelectListItem { Text = "Mexico", Value = "Mexico" }
            };

            model.RoleList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Admin", Value = "Admin" },
                new SelectListItem { Text = "User", Value = "User" },
                new SelectListItem { Text = "Guest", Value = "Guest" }
            }; ;
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new User(0, viewModel.Username, viewModel.Name, viewModel.Email)
                    {
                        Country = viewModel.Country,
                        Active = viewModel.IsActive,
                    };

                    if (viewModel.Roles != null
                        && viewModel.Roles.Count > 0)
                    {
                        user.Roles.AddRange(viewModel.Roles);
                    }

                    _logger.LogInformation("Creating user {0}", user.UserName);
                    await _usersService.AddAsync(user);
                    _logger.LogInformation("User {0} created", user.UserName);
                    TempData["Message"] = "User created successfully";
                    return RedirectToAction(nameof(Index));
                }

                _logger.LogError("Invalid model state");
                ViewBag.Message = "Invalid model state";

            }
            catch(ArgumentOutOfRangeException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                _logger.LogError("Invalid model state");
                ViewBag.Message = "Invalid model state";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                ViewBag.Message = "Error creating user";
            }
            

            FillLookups(viewModel);
            return View(viewModel);
        }

        // GET: UsersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var user = await _usersService.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserViewModel
            {                
                Username = user.UserName,
                Name = user.Name,
                Email = user.Email,
                Country = user.Country,
                IsActive = user.Active,
                Roles = user.Roles
            };

            FillLookups(viewModel);
            return View(viewModel);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UserViewModel viewModel)
        {
            try
            {              

                if (id <= 0)
                {
                    return BadRequest();
                }

                var user = await _usersService.GetAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    user.UpdateUser(user.UserName, viewModel.Name, viewModel.Email);
                    user.Country = viewModel.Country;
                    user.Active = viewModel.IsActive;
                    user.Roles.Clear();
                    if (viewModel.Roles != null
                                               && viewModel.Roles.Count > 0)
                    {
                        user.Roles.AddRange(viewModel.Roles);
                    }

                    await _usersService.UpdateAsync(user);
                    TempData["Message"] = "User updated successfully";
                    return RedirectToAction(nameof(Index));
                }

                
                _logger.LogError("Invalid model state");
                ViewBag.Message = "Invalid model state";

            }
            catch (ArgumentOutOfRangeException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                _logger.LogError("Invalid model state");
                ViewBag.Message = "Invalid model state";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user '{id}'");
                ViewBag.Message = "Error updating user";
            }


            FillLookups(viewModel);
            return View(viewModel);
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
