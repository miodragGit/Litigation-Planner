using System.Collections.Generic;

namespace LitigationPlanner.Data.Models.Entities
{
    public partial class ProcessType
    {
        public ProcessType()
        {
            Litigation = new HashSet<Litigation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Litigation> Litigation { get; set; }
    }
}
