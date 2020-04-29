using LitigationPlanner.Data.Models.DataTransferObjects;
using System.Collections.Generic;

namespace LitigationPlanner.MVC.Models
{
    public class UpdateContactViewModel
    {
        public ContactDto Contact { get; set; }
        public List<CompanyDto> Companies { get; set; }
    }
}
