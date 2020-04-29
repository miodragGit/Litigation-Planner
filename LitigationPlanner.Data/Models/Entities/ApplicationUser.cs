using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LitigationPlanner.Data.Models.Entities
{
    public partial class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            LitigationUser = new HashSet<LitigationUser>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<LitigationUser> LitigationUser { get; set; }
    }
}
