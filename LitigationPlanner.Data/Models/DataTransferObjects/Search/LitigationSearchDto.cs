using System;
using System.Collections.Generic;

namespace LitigationPlanner.Data.Models.DataTransferObjects.Search
{
    public class LitigationSearchDto
    {
        public DateTime? FromDateAndTime { get; set; }
        public DateTime? ToDateAndTime { get; set; }
        public int? LocationId { get; set; }
        public int? Judge { get; set; }
        public int? InstitutionType { get; set; }
        public string ProcessIdentifier { get; set; }
        public int? CourtroomNumber { get; set; }
        public int? Prosecutor { get; set; }
        public int? Defendant { get; set; }
        public int? ProcessType { get; set; }
        public List<string> UsersIds { get; set; }
        public string loggedUserUsername { get; set; }
    }
}
