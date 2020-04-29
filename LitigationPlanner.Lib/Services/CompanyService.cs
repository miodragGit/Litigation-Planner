using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Data.WorkUnitsInterfaces;
using LitigationPlanner.Lib.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Lib.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork;

        public CompanyService(ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork)
        {
            this.litigationPlannerUnitOfWork = litigationPlannerUnitOfWork;
        }

        public async Task<int> CreateAsync(CompanyDto companyDto)
        {
            var companyId = await litigationPlannerUnitOfWork.CompanyRepository.CreateAsync(companyDto);

            return companyId;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var isDeleted = await litigationPlannerUnitOfWork.CompanyRepository.DeleteAsync(id);

            return isDeleted;
        }

        public async Task<CompanyDto> GetAsync(int id)
        {
            var companyDto = await litigationPlannerUnitOfWork.CompanyRepository.GetAsync(id);

            return companyDto;
        }

        public async Task<List<CompanyDto>> GetAsync()
        {
            var companyDtos = await litigationPlannerUnitOfWork.CompanyRepository.GetAsync();

            return companyDtos;
        }

        public async Task<bool> UpdateAsync(CompanyDto companyDto)
        {
            var isUpdated = await litigationPlannerUnitOfWork.CompanyRepository.UpdateAsync(companyDto);

            return isUpdated;
        }

        public async Task<List<CompanyDto>> SearchCompaniesAsync(CompanySearchDto searchDto)
        {
            var companies = await litigationPlannerUnitOfWork.CompanyRepository.SearchCompaniesAsync(searchDto);

            return companies;
        }
    }
}
