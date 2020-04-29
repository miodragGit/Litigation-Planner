using System;
using System.Collections.Generic;

namespace LitigationPlanner.Data.Models.Entities
{
    public partial class Litigation
    {
        public Litigation()
        {
            LitigationUsers = new HashSet<LitigationUser>();
        }

        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public int LocationId { get; set; }
        public int? Judge { get; set; }
        public int? InstitutionType { get; set; }
        public string ProcessIdentifier { get; set; }
        public int? CourtroomNumber { get; set; }
        public int? Prosecutor { get; set; }
        public int? Defendant { get; set; }
        public string Note { get; set; }
        public int? ProcessType { get; set; }

        public virtual Contact DefendantNavigation { get; set; }
        public virtual Contact JudgeNavigation { get; set; }
        public virtual Location Location { get; set; }
        public virtual ProcessType ProcessTypeNavigation { get; set; }
        public virtual Contact ProsecutorNavigation { get; set; }
        public virtual ICollection<LitigationUser> LitigationUsers { get; set; }
    }
}
