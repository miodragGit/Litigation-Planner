using System.ComponentModel.DataAnnotations;

namespace LitigationPlanner.MVC.Models.ProcessType
{
    public class CreateProcessTypeViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
