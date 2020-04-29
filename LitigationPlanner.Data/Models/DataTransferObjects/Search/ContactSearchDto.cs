namespace LitigationPlanner.Data.Models.DataTransferObjects.Search
{
    public class ContactSearchDto
    {
        public string Name { get; set; }
        public string TelephoneNumber1 { get; set; }
        public string TelephoneNumber2 { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool LegalOrNaturalPerson { get; set; }
        public string Profession { get; set; }
        public int? CompanyId { get; set; }
    }
}
