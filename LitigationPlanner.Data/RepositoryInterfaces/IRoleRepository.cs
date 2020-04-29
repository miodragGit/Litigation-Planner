using LitigationPlanner.Data.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Data.RepositoryInterfaces
{
    public interface IRoleRepository
    {
        Task<string> CreateAsync(RoleDto roleDto);
        Task<bool> DeleteAsync(string id);
        Task<RoleDto> GetAsync(string id);
        Task<List<RoleDto>> GetAsync();
        Task<bool> UpdateAsync(RoleDto roleDto);
    }
}
