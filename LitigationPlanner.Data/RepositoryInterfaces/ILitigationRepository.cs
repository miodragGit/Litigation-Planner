using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Data.RepositoryInterfaces
{
    public interface ILitigationRepository
    {
        Task<int> CreateAsync(LitigationDto litigationDto, List<string> usersIds);
        Task<bool> DeleteAsync(int id);
        Task<bool> RemoveLitigationUsersAsync(int litigationId);
        Task<LitigationDto> GetAsync(int id);
        Task<List<LitigationDto>> GetAsync();
        Task<bool> UpdateAsync(LitigationDto litigationDto);
        Task<bool> AddUsersInLitigationAsync(int litigationId, IEnumerable<string> usersIds);
        Task<int> CreateLitigationUserAsync(LitigationUserDto litigationUserDto);
        Task<bool> DeleteLitigationUserAsync(List<LitigationUserDto> litigationUserDtos);
        Task<List<LitigationDto>> SearchLitigationsAsync(LitigationSearchDto searchDto);
        Task<List<LitigationDto>> GetLitigationsForUser(string userId);
        Task<List<LitigationDto>> OrderByDateAscending(bool isAscending, string userId = null);
    }
}
