using System.ComponentModel.DataAnnotations;

namespace LitigationPlanner.MVC.Models
{
    public class CreateCompanyViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
