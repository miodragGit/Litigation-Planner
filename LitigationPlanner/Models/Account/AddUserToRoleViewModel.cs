using LitigationPlanner.Data.Models.DataTransferObjects;
using System.Collections.Generic;

namespace LitigationPlanner.MVC.Models.Account
{
    public class AddUserToRoleViewModel
    {
        public string UserId { get; set; }
        public RoleDto Role { get; set; }
        
        public List<ApplicationUserDto> Users { get; set; }
    }
}
