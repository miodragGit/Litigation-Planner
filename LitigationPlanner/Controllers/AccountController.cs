using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Lib.ServiceInterfaces;
using LitigationPlanner.MVC.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LitigationPlanner.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly IRoleService roleService;
        private readonly ICompanyService companyService;
        private readonly IContactService contactService;
        private readonly ILitigationService litigationService;
        private readonly ILocationService locationService;
        private readonly IProcessTypeService processTypeService;

        public AccountController(IApplicationUserService applicationUserService,
                                 IRoleService roleService,
                                 ICompanyService companyService,
                                 IContactService contactService,
                                 ILitigationService litigationService,
                                 ILocationService locationService,
                                 IProcessTypeService processTypeService)
        {
            this.applicationUserService = applicationUserService;
            this.roleService = roleService;
            this.companyService = companyService;
            this.contactService = contactService;
            this.litigationService = litigationService;
            this.locationService = locationService;
            this.processTypeService = processTypeService;
        }

        [AllowAnonymous]
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View("Views/Account/Login.cshtml");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            LoginDto loginDto = new LoginDto
            {
                UserName = model.UserName,
                Password = model.Password
            };

            var result = await applicationUserService.SignInAsync(loginDto);

            if (result)
            {
                var loggedUser = await applicationUserService.GetByUsernameAsync(model.UserName);
                var isAdmin = await applicationUserService.IsUserInRoleAsync(loggedUser, "Administrator");

                if (isAdmin)
                {
                    return RedirectToAction("LitigationsList", "Litigation");
                }
                else
                {
                    return RedirectToAction("LitigationsListForUser", "Litigation");
                }
            }

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await applicationUserService.SignOutAsync();

            return RedirectToAction("Login");
        }

        [HttpGet("UsersList")]
        public async Task<IActionResult> UsersListAsync()
        {
            var users = await applicationUserService.GetAsync();

            var model = new UsersListViewModel()
            {
                Users = users
            };

            return View("Views/Account/UsersList.cshtml", model);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUserAsync(CreateUserViewModel model)
        {
            ApplicationUserDto user = new ApplicationUserDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            await applicationUserService.CreateAsync(user, model.Password);

            return RedirectToAction("UsersList", "Account");
        }

        [HttpPost("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            await applicationUserService.DeleteAsync(id);
           
            return RedirectToAction("UsersList", "Account");
        }

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserViewModel model)
        {
            ApplicationUserDto user = new ApplicationUserDto
            {
                Id = model.User.Id,
                FirstName = model.User.FirstName,
                LastName = model.User.LastName,
                UserName = model.User.UserName,
                Email = model.User.Email
            };

            await applicationUserService.UpdateAsync(user);

            return RedirectToAction("UsersList", "Account");
        }

        [HttpGet("RolesList")]
        public async Task<IActionResult> RolesListAsync()
        {
            var roles = await roleService.GetAsync();

            var model = new RolesListViewModel()
            {
                Roles = roles
            };

            model.Users = await applicationUserService.GetAsync();

            return View("Views/Account/RolesList.cshtml", model);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleViewModel model)
        {
            RoleDto role = new RoleDto
            {
                Name = model.Name
            };

            await roleService.CreateAsync(role);

            return RedirectToAction("RolesList", "Account");
        }

        [HttpPost("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRoleAsync(string id)
        {
            await roleService.DeleteAsync(id);

            return RedirectToAction("RolesList", "Account");
        }

        [HttpPost("UpdateRole")]
        public async Task<IActionResult> UpdateRoleAsync(UpdateRoleViewModel model)
        {
            RoleDto role = new RoleDto
            {
                Id = model.Role.Id,
                Name = model.Role.Name
            };

            await roleService.UpdateAsync(role);

            return RedirectToAction("RolesList", "Account");
        }

        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRoleAsync(AddUserToRoleViewModel model)
        {
            await applicationUserService.AddUserToRoleAsync(model.UserId, model.Role.Name);

            return RedirectToAction("RolesList", "Account");
        }

        [HttpPost("RemoveUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole(RemoveUserFromRoleViewModel model)
        {
            await applicationUserService.RemoveUserFromRoleAsync(model.UserId, model.Role.Name);

            return RedirectToAction("RolesList", "Account");
        }
    }
}