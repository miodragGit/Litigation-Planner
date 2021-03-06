﻿using LitigationPlanner.Data.Models.DataTransferObjects;
using System.Collections.Generic;

namespace LitigationPlanner.MVC.Models.Litigation
{
    public class LitigationsListViewModel
    {
        public List<LitigationDto> Litigations { get; set; }
        public List<ContactDto> Contacts { get; set; }
        public List<LocationDto> Locations { get; set; }
        public List<ProcessTypeDto> ProcessTypes { get; set; }
        public List<ApplicationUserDto> Users { get; set; }
    }
}
