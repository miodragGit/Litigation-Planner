using AutoMapper;
using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitigationPlanner.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public RoleRepository(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public async Task<string> CreateAsync(RoleDto roleDto)
        {
            var role = mapper.Map<IdentityRole>(roleDto);

            await roleManager.CreateAsync(role);

            return role.Id;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            await roleManager.DeleteAsync(role);

            return true;
        }

        public async Task<RoleDto> GetAsync(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            var roleDto = mapper.Map<RoleDto>(role);

            return roleDto;
        }

        public async Task<List<RoleDto>> GetAsync()
        {
            var query = roleManager.Roles
                .Select(r => mapper.Map<RoleDto>(r));

            var roleDtos = await query.ToListAsync();

            return roleDtos;
        }

        public async Task<bool> UpdateAsync(RoleDto roleDto)
        {
            var role = await roleManager.FindByIdAsync(roleDto.Id);

            role.Name = roleDto.Name;
  
            await roleManager.UpdateAsync(role);

            return true;
        }
    }
}
