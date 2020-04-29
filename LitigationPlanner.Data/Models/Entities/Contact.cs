using System.Collections.Generic;

namespace LitigationPlanner.Data.Models.Entities
{
    public partial class Contact
    {
        public Contact()
        {
            LitigationDefendantNavigation = new HashSet<Litigation>();
            LitigationJudgeNavigation = new HashSet<Litigation>();
            LitigationProsecutorNavigation = new HashSet<Litigation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string TelephoneNumber1 { get; set; }
        public string TelephoneNumber2 { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool LegalOrNaturalPerson { get; set; }
        public string Profession { get; set; }
        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Litigation> LitigationDefendantNavigation { get; set; }
        public virtual ICollection<Litigation> LitigationJudgeNavigation { get; set; }
        public virtual ICollection<Litigation> LitigationProsecutorNavigation { get; set; }
    }
}
