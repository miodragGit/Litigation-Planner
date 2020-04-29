using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Lib.ServiceInterfaces;
using LitigationPlanner.Models;
using LitigationPlanner.MVC.Models;
using LitigationPlanner.MVC.Models.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LitigationPlanner.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CompanyController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly ICompanyService companyService;
        private readonly IContactService contactService;
        private readonly ILitigationService litigationService;
        private readonly ILocationService locationService;
        private readonly IProcessTypeService processTypeService;

        public CompanyController(IApplicationUserService applicationUserService,
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

        [HttpGet("CompaniesList")]
        public async Task<IActionResult> CompaniesListAsync()
        {
            try
            {
                var companies = await companyService.GetAsync();

                var model = new CompaniesListViewModel()
                {
                    Companies = companies
                };

                return View("Views/Company/CompaniesList.cshtml", model);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpPost("CreateCompany")]
        public async Task<IActionResult> CreateCompanyAsync(CreateCompanyViewModel model)
        {
            try
            {
                CompanyDto company = new CompanyDto
                {
                    Title = model.Title,
                    Address = model.Address
                };

                await companyService.CreateAsync(company);

                return RedirectToAction("CompaniesList", "Company");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpPost("DeleteCompany/{id}")]
        public async Task<IActionResult> DeleteCompanyAsync(int id)
        {
            try
            {
                await companyService.DeleteAsync(id);

                return RedirectToAction("CompaniesList", "Company");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpPost("UpdateCompany")]
        public async Task<IActionResult> UpdateCompanyAsync(UpdateCompanyViewModel model)
        {
            try
            {
                CompanyDto company = new CompanyDto
                {
                    Id = model.Company.Id,
                    Title = model.Company.Title,
                    Address = model.Company.Address
                };

                await companyService.UpdateAsync(company);

                return RedirectToAction("CompaniesList", "Company");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpGet("SearchCompanies")]
        public async Task<IActionResult> SearchCompaniesAsync(SearchCompaniesViewModel searchModel)
        {
            try
            {
                CompanySearchDto searchDto = new CompanySearchDto
                {
                    Title = searchModel.Title,
                    Address = searchModel.Address
                };

                var companies = await companyService.SearchCompaniesAsync(searchDto);

                CompaniesListViewModel viewModel = new CompaniesListViewModel()
                {
                    Companies = companies
                };

                return View("Views/Company/CompaniesList.cshtml", viewModel);
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