using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Data.RepositoryInterfaces
{
    public interface ILocationRepository
    {
        Task<int> CreateAsync(LocationDto locationDto);
        Task<bool> DeleteAsync(int id);
        Task<LocationDto> GetAsync(int id);
        Task<List<LocationDto>> GetAsync();
        Task<bool> UpdateAsync(LocationDto locationDto);
        Task<List<LocationDto>> SearchLocatonsAsync(LocationSearchDto searchDto);
    }
}
