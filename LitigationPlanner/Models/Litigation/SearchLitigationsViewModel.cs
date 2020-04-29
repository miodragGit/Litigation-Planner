using LitigationPlanner.Data.Models.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace LitigationPlanner.MVC.Models.Litigation
{
    public class SearchLitigationsViewModel
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

        public List<ContactDto> Contacts { get; set; }
        public List<LocationDto> Locations { get; set; }
        public List<ProcessTypeDto> ProcessTypes { get; set; }
        public List<ApplicationUserDto> Users { get; set; }
    }
}
