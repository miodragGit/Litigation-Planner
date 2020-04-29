using System.ComponentModel.DataAnnotations;

namespace LitigationPlanner.MVC.Models.Account
{
    public class CreateUserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
