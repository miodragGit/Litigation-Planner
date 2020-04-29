using AutoMapper;
using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.Entities;
using LitigationPlanner.Data.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitigationPlanner.Data.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public ApplicationUserRepository(UserManager<ApplicationUser> userManager,
                                         SignInManager<ApplicationUser> signInManager,
                                         IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task<bool> SignInAsync(LoginDto loginDto)
        {
            var result = await signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, true, true);

            if (result.Succeeded)
                return true;

            return false;
        }

        public async Task<bool> SignOutAsync()
        {
            await signInManager.SignOutAsync();
            
            return true;
        }

        public async Task<ApplicationUserDto> CreateAsync(ApplicationUserDto userDto, string password)
        {
            var user = mapper.Map<ApplicationUser>(userDto);

            await userManager.CreateAsync(user, password);

            return mapper.Map<ApplicationUserDto>(user);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            await userManager.DeleteAsync(user);

            return true;
        }

        public async Task<ApplicationUserDto> GetAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userDto = mapper.Map<ApplicationUserDto>(user);

            return userDto;
        }

        public async Task<ApplicationUserDto> GetByUsernameAsync(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            var userDto = mapper.Map<ApplicationUserDto>(user);

            return userDto;
        }

        public async Task<List<ApplicationUserDto>> GetAsync()
        {
            var query = userManager.Users
                .Select(u => mapper.Map<ApplicationUserDto>(u));

            var userDtos = await query.ToListAsync();

            return userDtos;
        }

        public async Task<bool> UpdateAsync(ApplicationUserDto userDto)
        {
            var user = await userManager.FindByIdAsync(userDto.Id);

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
         
            await userManager.UpdateAsync(user);
           
            return true;
        }

        public async Task<bool> AddUserToRoleAsync(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);

            await userManager.AddToRoleAsync(user, roleName);

            return true;
        }

        public async Task<bool> RemoveUserFromRoleAsync(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);

            await userManager.RemoveFromRoleAsync(user, roleName);

            return true;
        }

        public async Task<bool> IsUserInRoleAsync(ApplicationUserDto userDto, string roleName)
        {
            var user = mapper.Map<ApplicationUser>(userDto);
            var isInRole = await userManager.IsInRoleAsync(user, roleName);
            
            return isInRole;
        }
    }
}
