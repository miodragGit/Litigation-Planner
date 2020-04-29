using AutoMapper;
using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LitigationPlanner.Data.Mapping
{
    public class EntitiesMapper : Profile
    {
        public EntitiesMapper()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();

            CreateMap<ProcessType, ProcessTypeDto>();
            CreateMap<ProcessTypeDto, ProcessType>();

            CreateMap<Location, LocationDto>();
            CreateMap<LocationDto, Location>();

            CreateMap<Contact, ContactDto>();
            CreateMap<ContactDto, Contact>();

            CreateMap<Litigation, LitigationDto>();
            CreateMap<LitigationDto, Litigation>();

            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<ApplicationUserDto, ApplicationUser>();

            CreateMap<IdentityRole, RoleDto>();
            CreateMap<RoleDto, IdentityRole>();

            CreateMap<LitigationUser, LitigationUserDto>();
            CreateMap<LitigationUserDto, LitigationUser>();
        }
    }
}
