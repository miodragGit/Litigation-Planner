using System.ComponentModel.DataAnnotations;

namespace LitigationPlanner.MVC.Models.Location
{
    public class CreateLocationViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
