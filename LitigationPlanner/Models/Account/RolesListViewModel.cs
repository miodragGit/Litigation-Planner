using LitigationPlanner.Data.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitigationPlanner.MVC.Models.Account
{
    public class RolesListViewModel
    {
        public List<RoleDto> Roles { get; set; }

        public List<ApplicationUserDto> Users { get; set; }
    }
}
