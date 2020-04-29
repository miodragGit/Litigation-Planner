using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Lib.ServiceInterfaces;
using LitigationPlanner.Models;
using LitigationPlanner.MVC.Models.Location;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LitigationPlanner.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LocationController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly ICompanyService companyService;
        private readonly IContactService contactService;
        private readonly ILitigationService litigationService;
        private readonly ILocationService locationService;
        private readonly IProcessTypeService processTypeService;

        public LocationController(IApplicationUserService applicationUserService,
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

        [HttpGet("LocationsLis")]
        public async Task<IActionResult> LocationsListAsync()
        {
            try
            {
                var locations = await locationService.GetAsync();

                var model = new LocationsListViewModel()
                {
                    Locations = locations
                };

                return View("Views/Location/LocationsList.cshtml", model);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpPost("CreateLocation")]
        public async Task<IActionResult> CreateLocationAsync(CreateLocationViewModel model)
        {
            try
            {
                LocationDto location = new LocationDto
                {
                    Title = model.Title
                };

                await locationService.CreateAsync(location);

                return RedirectToAction("LocationsList", "Location");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpPost("DeleteLocation/{id}")]
        public async Task<IActionResult> DeleteLocationAsync(int id)
        {
            try
            {
                await locationService.DeleteAsync(id);

                return RedirectToAction("LocationsList", "Location");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpPost("UpdateLocation")]
        public async Task<IActionResult> UpdateLocationAsync(UpdateLocationViewModel model)
        {
            try
            {
                LocationDto location = new LocationDto
                {
                    Id = model.Location.Id,
                    Title = model.Location.Title
                };

                await locationService.UpdateAsync(location);

                return RedirectToAction("LocationsList", "Location");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpGet("SearchLocations")]
        public async Task<IActionResult> SearchLocationsAsync(SearchLocationsViewModel searchModel)
        {
            try
            {
                LocationSearchDto searchDto = new LocationSearchDto
                {
                    Title = searchModel.Title
                };

                var locations = await locationService.SearchLocationsAsync(searchDto);

                LocationsListViewModel viewModel = new LocationsListViewModel()
                {
                    Locations = locations
                };

                return View("Views/Location/LocationsList.cshtml", viewModel);
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