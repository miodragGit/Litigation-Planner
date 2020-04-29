using LitigationPlanner.Data.Models.Entities;
using System.Collections.Generic;

namespace LitigationPlanner.Data.Models.DataTransferObjects
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Contact> Contact { get; set; }
    }
}
