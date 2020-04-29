using Microsoft.AspNetCore.Identity;

namespace LitigationPlanner.Data.Models.DataTransferObjects
{
    public class ApplicationUserDto : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
