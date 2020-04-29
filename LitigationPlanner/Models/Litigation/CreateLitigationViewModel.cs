using LitigationPlanner.Data.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LitigationPlanner.MVC.Models.Litigation
{
    public class CreateLitigationViewModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateAndTime { get; set; }
        [Required]
        public int LocationId { get; set; }
        public int? Judge { get; set; }
        public int? InstitutionType { get; set; }
        public string ProcessIdentifier { get; set; }
        public int? CourtroomNumber { get; set; }
        public int? Prosecutor { get; set; }
        public int? Defendant { get; set; }
        public string Note { get; set; }
        public int? ProcessType { get; set; }
        public List<ContactDto> Contacts { get; set; }
        public List<LocationDto> Locations { get; set; }
        public List<ProcessTypeDto> ProcessTypes { get; set; }
        public List<ApplicationUserDto> Users { get; set; }
        [Required]
        public List<string> UsersIds { get; set; }

    }
}
