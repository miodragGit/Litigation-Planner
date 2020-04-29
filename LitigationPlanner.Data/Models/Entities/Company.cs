using System.Collections.Generic;

namespace LitigationPlanner.Data.Models.Entities
{
    public partial class Company
    {
        public Company()
        {
            Contact = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Contact> Contact { get; set; }
    }
}
