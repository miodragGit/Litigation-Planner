using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Data.WorkUnitsInterfaces;
using LitigationPlanner.Lib.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Lib.Services
{
    public class LitigationService : ILitigationService
    {
        private readonly ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork;

        public LitigationService(ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork)
        {
            this.litigationPlannerUnitOfWork = litigationPlannerUnitOfWork;
        }
        public async Task<int> CreateAsync(LitigationDto litigationDto, List<string> usersIds)
        {
            var litigationId = await litigationPlannerUnitOfWork.LitigationRepository.CreateAsync(litigationDto, usersIds);

            return litigationId;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var transaction = await litigationPlannerUnitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await litigationPlannerUnitOfWork.LitigationRepository.DeleteAsync(id);
                    await litigationPlannerUnitOfWork.LitigationRepository.RemoveLitigationUsersAsync(id);

                    transaction.Commit();

                    return true;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<LitigationDto> GetAsync(int id)
        {
            var litigationDto = await litigationPlannerUnitOfWork.LitigationRepository.GetAsync(id);

            return litigationDto;
        }

        public async Task<List<LitigationDto>> GetAsync()
        {
            var litigationDtos = await litigationPlannerUnitOfWork.LitigationRepository.GetAsync();

            return litigationDtos;
        }

        public async Task<bool> UpdateAsync(LitigationDto litigationDto, IEnumerable<string> usersIds)
        {
            using(var transaction = await litigationPlannerUnitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await litigationPlannerUnitOfWork.LitigationRepository.UpdateAsync(litigationDto);

                    if (usersIds != null)
                        await litigationPlannerUnitOfWork.LitigationRepository.AddUsersInLitigationAsync(litigationDto.Id, usersIds);

                    transaction.Commit();

                    return true;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<int> CreateLitigationUserAsync(LitigationUserDto litigationUserDto)
        {
            var litigationUserId = await litigationPlannerUnitOfWork.LitigationRepository.CreateLitigationUserAsync(litigationUserDto);

            return litigationUserId;
        }

        public async Task<bool> DeleteLitigationUserAsync(List<LitigationUserDto> litigationUserDtos)
        {
            var isDeleted = await litigationPlannerUnitOfWork.LitigationRepository.DeleteLitigationUserAsync(litigationUserDtos);

            return isDeleted;
        }

        public async Task<List<LitigationDto>> SearchLitigationsAsync(LitigationSearchDto searchDto)
        {
            var loggedUser = await litigationPlannerUnitOfWork.ApplicationUserRepository.GetByUsernameAsync(searchDto.loggedUserUsername);

            if (searchDto.UsersIds == null)
                searchDto.UsersIds = new List<string>();

            searchDto.UsersIds.Add(loggedUser.Id);

            var litigations = await litigationPlannerUnitOfWork.LitigationRepository.SearchLitigationsAsync(searchDto);

            return litigations;
        }

        public async Task<List<LitigationDto>> GetLitigationsForUser(string userId)
        {
            var litigations = await litigationPlannerUnitOfWork.LitigationRepository.GetLitigationsForUser(userId);

            return litigations;
        }

        public async Task<List<LitigationDto>> OrderByDateAscending(bool isAscending, string userId = null)
        {
            List<LitigationDto> orderedLitigations = new List<LitigationDto>();
            if (userId == null)
            {
                orderedLitigations = await litigationPlannerUnitOfWork.LitigationRepository.OrderByDateAscending(isAscending);
            }
            else
            {
                orderedLitigations = await litigationPlannerUnitOfWork.LitigationRepository.OrderByDateAscending(isAscending, userId);
            }

            return orderedLitigations;
        }
    }
}
