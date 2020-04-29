using System;
using System.Collections.Generic;

namespace LitigationPlanner.Data.Models.DataTransferObjects
{
    public class LitigationDto
    {
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
        public ContactDto DefendantNavigation { get; set; }
        public ContactDto JudgeNavigation { get; set; }
        public LocationDto Location { get; set; }
        public ProcessTypeDto ProcessTypeNavigation { get; set; }
        public ContactDto ProsecutorNavigation { get; set; }
        public ICollection<LitigationUserDto> LitigationUsers { get; set; }
    }
}
