using AutoMapper;
using LitigationPlanner.Data.DbContexts;
using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Data.Models.Entities;
using LitigationPlanner.Data.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitigationPlanner.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly LitigationPlannerDBContext dbContext;
        private readonly IMapper mapper;

        public ContactRepository(LitigationPlannerDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<int> CreateAsync(ContactDto contactDto)
        {
            var contact = mapper.Map<Contact>(contactDto);

            await dbContext.Contact.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return contact.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contact = await dbContext
                .Contact
                .FirstOrDefaultAsync(c => c.Id == id);

            dbContext.Contact.Remove(contact);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<ContactDto> GetAsync(int id)
        {
            var contact = await dbContext
                .Contact
                .Include(c => c.Company)
                .FirstOrDefaultAsync(c => c.Id == id);
            var contactDto = mapper.Map<ContactDto>(contact);

            return contactDto;
        }

        public async Task<List<ContactDto>> GetAsync()
        {
            var query = dbContext.Contact
                .Include(c => c.Company)
                .Select(c => mapper.Map<ContactDto>(c));

            var contactDtos = await query.ToListAsync();

            return contactDtos;
        }

        public async Task<bool> UpdateAsync(ContactDto contactDto)
        {
            var contact = await dbContext
                .Contact
                .FirstOrDefaultAsync(c => c.Id == contactDto.Id);

            contact.Name = contactDto.Name;
            contact.TelephoneNumber1 = contactDto.TelephoneNumber1;
            contact.TelephoneNumber2 = contactDto.TelephoneNumber2;
            contact.Address = contactDto.Address;
            contact.Email = contactDto.Email;
            contact.LegalOrNaturalPerson = contactDto.LegalOrNaturalPerson;
            contact.Profession = contactDto.Profession;
            contact.CompanyId = contactDto.CompanyId;
      
            dbContext.Contact.Update(contact);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<ContactDto>> SearchContactsAsync(ContactSearchDto searchDto)
        {
            var contacts = await dbContext.Contact.Where(c => ((searchDto.Name == null) || ((searchDto.Name != null) && c.Name == searchDto.Name))
                                                   && ((searchDto.Address == null) || ((searchDto.Address != null) && c.Address == searchDto.Address))
                                                   && ((searchDto.Email == null) || ((searchDto.Email != null) && c.Email == searchDto.Email))
                                                   && ((searchDto.Profession == null) || ((searchDto.Profession != null) && c.Profession == searchDto.Profession))
                                                   && (((searchDto.CompanyId == null) || (searchDto.CompanyId == 0)) || ((searchDto.CompanyId != null) && c.CompanyId == searchDto.CompanyId))
                                                   && ((searchDto.TelephoneNumber2 == null) || ((searchDto.TelephoneNumber2 != null) && c.TelephoneNumber2 == searchDto.TelephoneNumber2))
                                                   && ((searchDto.TelephoneNumber1 == null) || ((searchDto.TelephoneNumber1 != null) && c.TelephoneNumber1 == searchDto.TelephoneNumber1)))
                .Include(c => c.Company)
                .ToListAsync();

            var contactsDtos = contacts.Select(c => mapper.Map<ContactDto>(c)).ToList();

            return contactsDtos;
        }
    }
}
