using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Lib.ServiceInterfaces
{
    public interface IProcessTypeService
    {
        Task<int> CreateAsync(ProcessTypeDto processTypeDto);
        Task<bool> DeleteAsync(int id);
        Task<ProcessTypeDto> GetAsync(int id);
        Task<List<ProcessTypeDto>> GetAsync();
        Task<bool> UpdateAsync(ProcessTypeDto processTypeDto);
        Task<List<ProcessTypeDto>> SearchProcessTypesAsync(ProcessTypeSearchDto searchDto);
    }
}
