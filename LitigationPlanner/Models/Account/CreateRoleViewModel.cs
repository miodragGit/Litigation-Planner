using System.ComponentModel.DataAnnotations;

namespace LitigationPlanner.MVC.Models.Account
{
    public class CreateRoleViewModel
    {
        [Required]
        public string Name { get; set; }
      
    }
}
