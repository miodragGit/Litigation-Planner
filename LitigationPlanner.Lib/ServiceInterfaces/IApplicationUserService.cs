using LitigationPlanner.Data.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Lib.ServiceInterfaces
{
    public interface IApplicationUserService
    {
        Task<bool> SignInAsync(LoginDto loginDto);
        Task<bool> SignOutAsync();
        Task<ApplicationUserDto> CreateAsync(ApplicationUserDto applicationUserDto, string password);
        Task<bool> DeleteAsync(string id);
        Task<ApplicationUserDto> GetAsync(string id);
        Task<ApplicationUserDto> GetByUsernameAsync(string username);
        Task<List<ApplicationUserDto>> GetAsync();
        Task<bool> UpdateAsync(ApplicationUserDto applicationUserDto);
        Task<bool> AddUserToRoleAsync(string userId, string roleName);
        Task<bool> RemoveUserFromRoleAsync(string userId, string roleName);
        Task<bool> IsUserInRoleAsync(ApplicationUserDto userDto, string roleName);
    }
}
