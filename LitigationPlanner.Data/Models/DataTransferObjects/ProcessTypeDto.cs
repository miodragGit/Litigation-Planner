using LitigationPlanner.Data.Models.Entities;
using System.Collections.Generic;

namespace LitigationPlanner.Data.Models.DataTransferObjects
{
    public class ProcessTypeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Litigation> Litigation { get; set; }
    }
}
