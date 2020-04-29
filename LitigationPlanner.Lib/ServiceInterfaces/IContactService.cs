﻿using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Lib.ServiceInterfaces
{
    public interface IContactService
    {
        Task<int> CreateAsync(ContactDto contactDto);
        Task<bool> DeleteAsync(int id);
        Task<ContactDto> GetAsync(int id);
        Task<List<ContactDto>> GetAsync();
        Task<bool> UpdateAsync(ContactDto contactDto);
        Task<List<ContactDto>> SearchContactsAsync(ContactSearchDto searchDto);
    }
}
