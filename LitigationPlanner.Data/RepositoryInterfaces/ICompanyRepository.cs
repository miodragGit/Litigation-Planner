using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Data.RepositoryInterfaces
{
    public interface ICompanyRepository
    {
        Task<int> CreateAsync(CompanyDto companyDto);
        Task<bool> DeleteAsync(int id);
        Task<CompanyDto> GetAsync(int id);
        Task<List<CompanyDto>> GetAsync();
        Task<bool> UpdateAsync(CompanyDto companyDto);
        Task<List<CompanyDto>> SearchCompaniesAsync(CompanySearchDto searchDto);
    }
}
