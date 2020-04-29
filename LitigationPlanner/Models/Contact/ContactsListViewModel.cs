using LitigationPlanner.Data.Models.DataTransferObjects;
using System.Collections.Generic;

namespace LitigationPlanner.MVC.Models
{
    public class ContactsListViewModel
    {
        public List<ContactDto> Contacts { get; set; }
        public List<CompanyDto> Companies { get; set; }
    }
}
