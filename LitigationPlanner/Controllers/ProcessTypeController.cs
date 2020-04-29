using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Lib.ServiceInterfaces;
using LitigationPlanner.Models;
using LitigationPlanner.MVC.Models.ProcessType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LitigationPlanner.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProcessTypeController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly ICompanyService companyService;
        private readonly IContactService contactService;
        private readonly ILitigationService litigationService;
        private readonly ILocationService locationService;
        private readonly IProcessTypeService processTypeService;

        public ProcessTypeController(IApplicationUserService applicationUserService,
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

        [HttpGet("ProcessTypesList")]
        public async Task<IActionResult> ProcessTypesListAsync()
        {
            try
            {
                var processTypes = await processTypeService.GetAsync();

                var model = new ProcessTypesListViewModel()
                {
                    ProcessTypes = processTypes
                };

                return View("Views/ProcessType/ProcessTypesList.cshtml", model);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpPost("CreateProcessType")]
        public async Task<IActionResult> CreateProcessTypeAsync(CreateProcessTypeViewModel model)
        {
            try
            {
                ProcessTypeDto processType = new ProcessTypeDto
                {
                    Title = model.Title
                };

                await processTypeService.CreateAsync(processType);

                return RedirectToAction("ProcessTypesList", "ProcessType");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpPost("DeleteProcessType")]
        public async Task<IActionResult> DeleteProcessTypeAsync(int id)
        {
            try
            {
                await processTypeService.DeleteAsync(id);

                return RedirectToAction("ProcessTypesList", "ProcessType");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpPost("UpdateProcessType")]
        public async Task<IActionResult> UpdateProcessTypeAsync(UpdateProcessTypeViewModel model)
        {
            try
            {          
                ProcessTypeDto processType = new ProcessTypeDto
                {
                    Id = model.ProcessType.Id,
                    Title = model.ProcessType.Title
                };

                await processTypeService.UpdateAsync(processType);

                return RedirectToAction("ProcessTypesList", "ProcessType");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpGet("SearchProcessTypes")]
        public async Task<IActionResult> SearchProcessTypesAsync(SearchProcessTypesViewModel searchModel)
        {
            try
            {
                ProcessTypeSearchDto searchDto = new ProcessTypeSearchDto
                {
                    Title = searchModel.Title
                };

                var processTypes = await processTypeService.SearchProcessTypesAsync(searchDto);

                ProcessTypesListViewModel viewModel = new ProcessTypesListViewModel()
                {
                    ProcessTypes = processTypes
                };

                return View("Views/ProcessType/ProcessTypesList.cshtml", viewModel);
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