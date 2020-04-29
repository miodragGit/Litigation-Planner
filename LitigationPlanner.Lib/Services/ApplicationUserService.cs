using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.WorkUnitsInterfaces;
using LitigationPlanner.Lib.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Lib.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork;

        public ApplicationUserService(ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork)
        {
            this.litigationPlannerUnitOfWork = litigationPlannerUnitOfWork;
        }

        public async Task<bool> SignInAsync(LoginDto loginDto)
        {
            var signedIn = await litigationPlannerUnitOfWork.ApplicationUserRepository.SignInAsync(loginDto);

            return signedIn;
        }

        public async Task<bool> SignOutAsync()
        {
            var signedOut = await litigationPlannerUnitOfWork.ApplicationUserRepository.SignOutAsync();

            return signedOut;
        }

        public async Task<ApplicationUserDto> CreateAsync(ApplicationUserDto userDto, string password)
        {
            var createdUserDto = await litigationPlannerUnitOfWork.ApplicationUserRepository.CreateAsync(userDto, password);

            return createdUserDto;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var isDeleted = await litigationPlannerUnitOfWork.ApplicationUserRepository.DeleteAsync(id);

            return isDeleted;
        }

        public async Task<ApplicationUserDto> GetAsync(string id)
        {
            var userDto = await litigationPlannerUnitOfWork.ApplicationUserRepository.GetAsync(id);

            return userDto;
        }

        public async Task<ApplicationUserDto> GetByUsernameAsync(string username)
        {
            var userDto = await litigationPlannerUnitOfWork.ApplicationUserRepository.GetByUsernameAsync(username);

            return userDto;
        }

        public async Task<List<ApplicationUserDto>> GetAsync()
        {
            var userDtos = await litigationPlannerUnitOfWork.ApplicationUserRepository.GetAsync();

            return userDtos;
        }

        public async Task<bool> UpdateAsync(ApplicationUserDto userDto)
        {
            var isUpdated = await litigationPlannerUnitOfWork.ApplicationUserRepository.UpdateAsync(userDto);

            return isUpdated;
        }

        public async Task<bool> AddUserToRoleAsync(string userId, string roleName)
        {
            var addedToRole = await litigationPlannerUnitOfWork.ApplicationUserRepository.AddUserToRoleAsync(userId, roleName);

            return addedToRole;
        }

        public async Task<bool> RemoveUserFromRoleAsync(string userId, string roleName)
        {
            var removedFromRole = await litigationPlannerUnitOfWork.ApplicationUserRepository.RemoveUserFromRoleAsync(userId, roleName);

            return removedFromRole;
        }

        public async Task<bool> IsUserInRoleAsync(ApplicationUserDto userDto, string roleName)
        {
            var isInRole = await litigationPlannerUnitOfWork.ApplicationUserRepository.IsUserInRoleAsync(userDto, roleName);

            return isInRole;
        }
    }
}
