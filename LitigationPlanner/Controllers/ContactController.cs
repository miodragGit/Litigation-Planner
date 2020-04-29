using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Lib.ServiceInterfaces;
using LitigationPlanner.Models;
using LitigationPlanner.MVC.Models;
using LitigationPlanner.MVC.Models.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LitigationPlanner.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ContactController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly ICompanyService companyService;
        private readonly IContactService contactService;
        private readonly ILitigationService litigationService;
        private readonly ILocationService locationService;
        private readonly IProcessTypeService processTypeService;

        public ContactController(IApplicationUserService applicationUserService,
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

        [HttpGet("ContactsList")]
        public async Task<IActionResult> ContactsListAsync()
        {
            try
            {
                var contacts = await contactService.GetAsync();

                var model = new ContactsListViewModel()
                {
                    Contacts = contacts
                };

                model.Companies = await companyService.GetAsync();

                return View("Views/Contact/ContactsList.cshtml", model);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpPost("CreateContact")]
        public async Task<IActionResult> CreateContactAsync(CreateContactViewModel model)
        {
            try
            {
                ContactDto contact = new ContactDto
                {
                    Name = model.Name,
                    TelephoneNumber1 = model.TelephoneNumber1,
                    TelephoneNumber2 = model.TelephoneNumber2,
                    Address = model.Address,
                    Email = model.Email,
                    LegalOrNaturalPerson = model.LegalOrNaturalPerson,
                    Profession = model.Profession,
                    CompanyId = model.CompanyId
                };

                await contactService.CreateAsync(contact);

                return RedirectToAction("ContactsList", "Contact");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpPost("DeleteContact/{id}")]
        public async Task<IActionResult> DeleteContactAsync(int id)
        {
            try
            {
                await contactService.DeleteAsync(id);

                return RedirectToAction("ContactsList", "Contact");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpPost("UpdateContact")]
        public async Task<IActionResult> UpdateContactAsync(UpdateContactViewModel model)
        {
            try
            {
                ContactDto contact = new ContactDto
                {
                    Id = model.Contact.Id,
                    Name = model.Contact.Name,
                    TelephoneNumber1 = model.Contact.TelephoneNumber1,
                    TelephoneNumber2 = model.Contact.TelephoneNumber2,
                    Address = model.Contact.Address,
                    Email = model.Contact.Email,
                    LegalOrNaturalPerson = model.Contact.LegalOrNaturalPerson,
                    Profession = model.Contact.Profession,
                    CompanyId = model.Contact.CompanyId
                };

                await contactService.UpdateAsync(contact);

                return RedirectToAction("ContactsList", "Contact");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.ErrorMessage = ex.Message.ToString();

                return View("Views/Shared/Error.cshtml", errorModel);
            }
        }

        [HttpGet("SearchContacts")]
        public async Task<IActionResult> SearchContactsAsync(SearchContactsViewModel searchModel)
        {
            try
            {
                ContactSearchDto searchDto = new ContactSearchDto
                {
                    Name = searchModel.Name,
                    TelephoneNumber1 = searchModel.TelephoneNumber1,
                    TelephoneNumber2 = searchModel.TelephoneNumber2,
                    Address = searchModel.Address,
                    Email = searchModel.Email,
                    Profession = searchModel.Profession,
                    CompanyId = searchModel.CompanyId
                };

                var contacts = await contactService.SearchContactsAsync(searchDto);

                ContactsListViewModel viewModel = new ContactsListViewModel()
                {
                    Contacts = contacts,
                    Companies = await companyService.GetAsync()
                };

                return View("Views/Contact/ContactsList.cshtml", viewModel);
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