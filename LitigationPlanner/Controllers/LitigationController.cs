using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Lib.ServiceInterfaces;
using LitigationPlanner.Models;
using LitigationPlanner.MVC.Models.Litigation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LitigationPlanner.MVC.Controllers
{
    public class LitigationController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly ICompanyService companyService;
        private readonly IContactService contactService;
        private readonly ILitigationService litigationService;
        private readonly ILocationService locationService;
        private readonly IProcessTypeService processTypeService;

        public LitigationController(IApplicationUserService applicationUserService,
                               ICompanyService companyService,
                               IContactService contactService,
                               ILitigationService litigationService,
                               ILocationService locationService,
                               IProcessTypeService processTypeService)
        {
            this.applicationUserService = applicationUserService;
            this.companyService = companyService;
            this.contactService = contactService;
            this.litigationService = litigationService;
            this.locationService = locationService;
            this.processTypeService = processTypeService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("LitigationsList")]
        public async Task<IActionResult> LitigationsListAsync()
        {
            try
            {
                var litigations = await litigationService.GetAsync();

                var model = new LitigationsListViewModel()
                {
                    Litigations = litigations
                };

                return View("Views/Litigation/LitigationsList.cshtml", model);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [Authorize(Roles = "NormalUser")]
        [HttpGet("LitigationsListForUser")]
        public async Task<IActionResult> LitigationsListForUserAsync()
        {
            try
            {
                var loggedUser = applicationUserService.GetByUsernameAsync(HttpContext.User.Identity.Name);
                var litigations = await litigationService.GetLitigationsForUser(loggedUser.Result.Id);

                var model = new LitigationsListViewModel()
                {
                    Litigations = litigations
                };

                model.Contacts = await contactService.GetAsync();
                model.Locations = await locationService.GetAsync();
                model.ProcessTypes = await processTypeService.GetAsync();
                model.Users = await applicationUserService.GetAsync();

                return View("Views/Litigation/LitigationsListForUser.cshtml", model);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [Authorize(Roles = "NormalUser")]
        [HttpPost("CreateLitigation")]
        public async Task<IActionResult> CreateLitigationAsync(CreateLitigationViewModel model)
        {
            try
            {
                LitigationDto litigation = new LitigationDto
                {
                    DateAndTime = model.DateAndTime,
                    LocationId = model.LocationId,
                    Judge = model.Judge,
                    InstitutionType = model.InstitutionType,
                    ProcessIdentifier = model.ProcessIdentifier,
                    CourtroomNumber = model.CourtroomNumber,
                    Prosecutor = model.Prosecutor,
                    Defendant = model.Defendant,
                    Note = model.Note,
                    ProcessType = model.ProcessType
                };

                await litigationService.CreateAsync(litigation, model.UsersIds);

                return RedirectToAction("LitigationsListForUser", "Litigation");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [Authorize(Roles = "NormalUser")]
        [HttpPost("DeleteLitigation/{id}")]
        public async Task<IActionResult> DeleteLitigationAsync(int id)
        {
            try
            {
                await litigationService.DeleteAsync(id);

                return RedirectToAction("LitigationsListForUser", "Litigation");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [Authorize(Roles = "NormalUser")]
        [HttpPost("UpdateLitigation")]
        public async Task<IActionResult> UpdateLitigationAsync(UpdateLitigationViewModel model)
        {
            try
            {
                LitigationDto litigation = new LitigationDto
                {
                    Id = model.Litigation.Id,
                    DateAndTime = model.Litigation.DateAndTime,
                    LocationId = model.Litigation.LocationId,
                    Judge = model.Litigation.Judge,
                    InstitutionType = model.Litigation.InstitutionType,
                    ProcessIdentifier = model.Litigation.ProcessIdentifier,
                    CourtroomNumber = model.Litigation.CourtroomNumber,
                    Prosecutor = model.Litigation.Prosecutor,
                    Defendant = model.Litigation.Defendant,
                    Note = model.Litigation.Note,
                    ProcessType = model.Litigation.ProcessType
                };

                await litigationService.UpdateAsync(litigation, model.UsersIds);

                return RedirectToAction("LitigationsListForUser", "Litigation");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpGet("UsersListForLitigation")]
        public async Task<IActionResult> UsersListForLitigationAsync(int id)
        {
            try
            {
                var litigation = await litigationService.GetAsync(id);

                List<ApplicationUserDto> model = new List<ApplicationUserDto>();
                foreach (var litigationUser in litigation.LitigationUsers)
                {
                    var user = await applicationUserService.GetAsync(litigationUser.UserId);
                    model.Add(user);
                }

                return PartialView("Views/Litigation/_UsersForLitigationPartial.cshtml", model);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [Authorize(Roles = "NormalUser")]
        [HttpGet("SearchLitigations")]
        public async Task<IActionResult> SearchLitigationsAsync(SearchLitigationsViewModel searchModel)
        {
            try
            {
                LitigationSearchDto searchDto = new LitigationSearchDto
                {
                    FromDateAndTime = searchModel.FromDateAndTime,
                    ToDateAndTime = searchModel.ToDateAndTime,
                    LocationId = searchModel.LocationId,
                    Judge = searchModel.Judge,
                    InstitutionType = searchModel.InstitutionType,
                    ProcessIdentifier = searchModel.ProcessIdentifier,
                    CourtroomNumber = searchModel.CourtroomNumber,
                    Prosecutor = searchModel.Prosecutor,
                    Defendant = searchModel.Defendant,
                    ProcessType = searchModel.ProcessType,
                    UsersIds = searchModel.UsersIds,
                    loggedUserUsername = HttpContext.User.Identity.Name
                };

                var litigations = await litigationService.SearchLitigationsAsync(searchDto);

                LitigationsListViewModel viewModel = new LitigationsListViewModel()
                {
                    Litigations = litigations,
                    Contacts = await contactService.GetAsync(),
                    Locations = await locationService.GetAsync(),
                    ProcessTypes = await processTypeService.GetAsync(),
                    Users = await applicationUserService.GetAsync()
                };

                return View("Views/Litigation/LitigationsListForUser.cshtml", viewModel);
            }
            catch(Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [Authorize(Roles = "NormalUser")]
        [HttpGet("CalendarPreview")]
        public IActionResult CalendarPreview()
        {
            return View();
        }

        [Authorize(Roles = "NormalUser")]
        [HttpGet]
        public async Task<IActionResult> GetLitigationsDates()
        {
            try
            {
                var loggedUser = applicationUserService.GetByUsernameAsync(HttpContext.User.Identity.Name);
                var litigations = await litigationService.GetLitigationsForUser(loggedUser.Result.Id);

                var litigationsDates = litigations.Where(l => l.DateAndTime.Month == DateTime.Now.Month).Select(l => l.DateAndTime.Day).ToList();

                return this.Json(new { litigationsDates = litigationsDates });
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> OrderByDateAscending(bool isAscending)
        {
            try
            {
                var user = await applicationUserService.GetByUsernameAsync(HttpContext.User.Identity.Name);
                var isAdmin = await applicationUserService.IsUserInRoleAsync(user, "Administrator");

                List<LitigationDto> orderedLitigations = new List<LitigationDto>();
                LitigationsListViewModel model = new LitigationsListViewModel();

                if (isAdmin)
                {
                    orderedLitigations = await litigationService.OrderByDateAscending(isAscending);

                    model.Litigations = orderedLitigations;
                }
                else
                {
                    orderedLitigations = await litigationService.OrderByDateAscending(isAscending, user.Id);

                    model.Litigations = orderedLitigations;
                    model.Contacts = await contactService.GetAsync();
                    model.Locations = await locationService.GetAsync();
                    model.ProcessTypes = await processTypeService.GetAsync();
                    model.Users = await applicationUserService.GetAsync();
                }

                if (isAdmin)
                    return View("Views/Litigation/LitigationsList.cshtml", model);

                return View("Views/Litigation/LitigationsListForUser.cshtml", model);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }
    }
}