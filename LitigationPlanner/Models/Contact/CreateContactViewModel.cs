using LitigationPlanner.Data.Models.DataTransferObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LitigationPlanner.MVC.Models
{
    public class CreateContactViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string TelephoneNumber1 { get; set; }
        public string TelephoneNumber2 { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool LegalOrNaturalPerson { get; set; }
        public string Profession { get; set; }
        public int? CompanyId { get; set; }
        public List<CompanyDto> Companies { get; set; }
    }
}
