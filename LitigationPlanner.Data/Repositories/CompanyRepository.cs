using AutoMapper;
using LitigationPlanner.Data.DbContexts;
using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Data.Models.Entities;
using LitigationPlanner.Data.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitigationPlanner.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly LitigationPlannerDBContext dbContext;
        private readonly IMapper mapper;

        public CompanyRepository(LitigationPlannerDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<int> CreateAsync(CompanyDto companyDto)
        {
            var company = mapper.Map<Company>(companyDto);

            await dbContext.Company.AddAsync(company);
            await dbContext.SaveChangesAsync();

            return company.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var company = await dbContext.Company.FirstOrDefaultAsync(c => c.Id == id);

            dbContext.Company.Remove(company);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<CompanyDto> GetAsync(int id)
        {
            var company = await dbContext.Company.FirstOrDefaultAsync(c => c.Id == id);
            var companyDto = mapper.Map<CompanyDto>(company);

            return companyDto;
        }

        public async Task<List<CompanyDto>> GetAsync()
        {
            try
            {
                var query = dbContext.Company
                .Select(c => mapper.Map<CompanyDto>(c));

                var companyDtos = await query.ToListAsync();

                return companyDtos;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(CompanyDto companyDto)
        {
            var company = await dbContext.Company.FirstOrDefaultAsync(c => c.Id == companyDto.Id);

            company.Title = companyDto.Title;
            company.Address = companyDto.Address;
            
            dbContext.Company.Update(company);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<CompanyDto>> SearchCompaniesAsync(CompanySearchDto searchDto)
        {
            var companies = await dbContext.Company.Where(c => ((searchDto.Title == null) || ((searchDto.Title != null) && c.Title == searchDto.Title))
                                                    && ((searchDto.Address == null) || ((searchDto.Address != null) && c.Address == searchDto.Address)))
                .ToListAsync();

            var companiesDtos = companies.Select(c => mapper.Map<CompanyDto>(c)).ToList();

            return companiesDtos;
        }
    }
}
