using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.WorkUnitsInterfaces;
using LitigationPlanner.Lib.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Lib.Services
{
    public class RoleService : IRoleService
    {
        private readonly ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork;

        public RoleService(ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork)
        {
            this.litigationPlannerUnitOfWork = litigationPlannerUnitOfWork;
        }

        public async Task<string> CreateAsync(RoleDto roleDto)
        {
            var roleId = await litigationPlannerUnitOfWork.RoleRepository.CreateAsync(roleDto);

            return roleId;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var isDeleted = await litigationPlannerUnitOfWork.RoleRepository.DeleteAsync(id);

            return isDeleted;
        }

        public async Task<RoleDto> GetAsync(string id)
        {
            var roleDto = await litigationPlannerUnitOfWork.RoleRepository.GetAsync(id);

            return roleDto;
        }

        public async Task<List<RoleDto>> GetAsync()
        {
            var roleDtos = await litigationPlannerUnitOfWork.RoleRepository.GetAsync();

            return roleDtos;
        }

        public async Task<bool> UpdateAsync(RoleDto roleDto)
        {
            var isUpdated = await litigationPlannerUnitOfWork.RoleRepository.UpdateAsync(roleDto);

            return isUpdated;
        }
    }
}
