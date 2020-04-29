namespace LitigationPlanner.Data.Models.Entities
{
    public partial class LitigationUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int LitigationId { get; set; }

        public virtual Litigation Litigation { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
