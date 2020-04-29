using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Data.WorkUnitsInterfaces;
using LitigationPlanner.Lib.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Lib.Services
{
    public class ContactService : IContactService
    {
        private readonly ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork;

        public ContactService(ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork)
        {
            this.litigationPlannerUnitOfWork = litigationPlannerUnitOfWork;
        }

        public async Task<int> CreateAsync(ContactDto contactDto)
        {
            var contactId = await litigationPlannerUnitOfWork.ContactRepository.CreateAsync(contactDto);

            return contactId;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var isDeleted = await litigationPlannerUnitOfWork.ContactRepository.DeleteAsync(id);

            return isDeleted;
        }

        public async Task<ContactDto> GetAsync(int id)
        {
            var contactDto = await litigationPlannerUnitOfWork.ContactRepository.GetAsync(id);

            return contactDto;
        }

        public async Task<List<ContactDto>> GetAsync()
        {
            var contactDtos = await litigationPlannerUnitOfWork.ContactRepository.GetAsync();

            return contactDtos;
        }

        public async Task<bool> UpdateAsync(ContactDto contactDto)
        {
            var isUpdated = await litigationPlannerUnitOfWork.ContactRepository.UpdateAsync(contactDto);

            return isUpdated;
        }

        public async Task<List<ContactDto>> SearchContactsAsync(ContactSearchDto searchDto)
        {
            var contacts = await litigationPlannerUnitOfWork.ContactRepository.SearchContactsAsync(searchDto);

            return contacts;
        }
    }
}
