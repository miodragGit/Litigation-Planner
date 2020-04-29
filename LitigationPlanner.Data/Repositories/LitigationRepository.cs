using AutoMapper;
using LitigationPlanner.Data.DbContexts;
using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Data.Models.Entities;
using LitigationPlanner.Data.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitigationPlanner.Data.Repositories
{
    public class LitigationRepository : ILitigationRepository
    {
        private readonly LitigationPlannerDBContext dbContext;
        private readonly IMapper mapper;

        public LitigationRepository(LitigationPlannerDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<int> CreateAsync(LitigationDto litigationDto, List<string> usersIds)
        {
            var litigation = mapper.Map<Litigation>(litigationDto);

            await dbContext.Litigation.AddAsync(litigation);

            await dbContext.SaveChangesAsync();

            var litigationUsers = usersIds
                .Select(p => new LitigationUser
                {
                    LitigationId = litigation.Id,
                    UserId = p
                });

            await dbContext.LitigationUser.AddRangeAsync(litigationUsers);

            await dbContext.SaveChangesAsync();

            return litigation.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var litigation = await dbContext
                .Litigation
                .FirstOrDefaultAsync(l => l.Id == id);

            dbContext.Litigation.Remove(litigation);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveLitigationUsersAsync(int litigationId)
        {
            var litigationUsers = dbContext.LitigationUser.Where(lu => lu.LitigationId == litigationId).ToList();
            dbContext.LitigationUser.RemoveRange(litigationUsers);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<LitigationDto> GetAsync(int id)
        {
            var litigation = await dbContext
                .Litigation
                .Include(l => l.Location)
                .Include(l => l.ProcessTypeNavigation)
                .Include(l => l.JudgeNavigation)
                .Include(l => l.DefendantNavigation)
                .Include(l => l.ProsecutorNavigation)
                .Include(l => l.LitigationUsers)
                .FirstOrDefaultAsync(l => l.Id == id);

            var litigationDto = mapper.Map<LitigationDto>(litigation);

            return litigationDto;
        }

        public async Task<List<LitigationDto>> GetAsync()
        {
            var query = dbContext
                .Litigation
                .Include(l => l.Location)
                .Include(l => l.ProcessTypeNavigation)
                .Include(l => l.JudgeNavigation)
                .Include(l => l.DefendantNavigation)
                .Include(l => l.ProsecutorNavigation)
                .Include(l => l.LitigationUsers)
                .Select(l => mapper.Map<LitigationDto>(l));

            var litigationDtos = await query.ToListAsync();
         
            return litigationDtos;
        }

        public async Task<bool> UpdateAsync(LitigationDto litigationDto)
        {
            var litigation = await dbContext
                .Litigation
                .FirstOrDefaultAsync(l => l.Id == litigationDto.Id);

            litigation.DateAndTime = litigationDto.DateAndTime;
            litigation.LocationId = litigationDto.LocationId;
            litigation.Judge = litigationDto.Judge;
            litigation.InstitutionType = litigationDto.InstitutionType;
            litigation.ProcessIdentifier = litigationDto.ProcessIdentifier;
            litigation.CourtroomNumber = litigationDto.CourtroomNumber;
            litigation.Prosecutor = litigationDto.Prosecutor;
            litigation.Defendant = litigationDto.Defendant;
            litigation.Note = litigationDto.Note;
            litigation.ProcessType = litigationDto.ProcessType;

            dbContext.Litigation.Update(litigation);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddUsersInLitigationAsync(int litigationId, IEnumerable<string> usersIds)
        {
            var litigationUsers = dbContext.LitigationUser.Where(lu => lu.LitigationId == litigationId).AsEnumerable();

            //LitigationUsers for Delete
            var userIdsForDelete = litigationUsers.Select(lu => lu.UserId).Except(usersIds).ToList();
            var litigationUsersForDelete = dbContext.LitigationUser.Where(lu => (userIdsForDelete.Contains(lu.UserId)) && lu.LitigationId == litigationId).AsEnumerable();
            dbContext.LitigationUser.RemoveRange(litigationUsersForDelete);

            //LitigationUsers for Add
            var userIdsForAdd = usersIds.Except(litigationUsers.Select(lu => lu.UserId));
            var litigationUsersForAdd = userIdsForAdd.Select(ui => new LitigationUser { LitigationId = litigationId, UserId = ui }).ToList();
            dbContext.LitigationUser.AddRange(litigationUsersForAdd);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<int> CreateLitigationUserAsync(LitigationUserDto litigationUserDto)
        {
            var litigatonUser = mapper.Map<LitigationUser>(litigationUserDto);

            await dbContext.LitigationUser.AddAsync(litigatonUser);
            await dbContext.SaveChangesAsync();

            return litigatonUser.Id;
        }

        public async Task<bool> DeleteLitigationUserAsync(List<LitigationUserDto> litigationUserDtos)
        {
            var litigationUsers = dbContext.LitigationUser.Where(lu => litigationUserDtos.Contains(mapper.Map<LitigationUserDto>(lu))).AsEnumerable();

            dbContext.LitigationUser.RemoveRange(litigationUsers);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<LitigationDto>> SearchLitigationsAsync(LitigationSearchDto searchDto)
        {
            var litigationUsers = dbContext.LitigationUser.Where(lu => searchDto.UsersIds.Contains(lu.UserId)).AsEnumerable();
            var litigationsIds = litigationUsers.Select(lu => lu.LitigationId);

            var litigations = await dbContext.Litigation.Where(l => (((searchDto.Defendant == null) || (searchDto.Defendant == 0)) || ((searchDto.Defendant != null) && l.Defendant == searchDto.Defendant))
                                                        && (((searchDto.Prosecutor == null) || (searchDto.Prosecutor == 0)) || ((searchDto.Prosecutor != null) && l.Defendant == searchDto.Prosecutor))
                                                        && (((searchDto.CourtroomNumber == null) || (searchDto.CourtroomNumber == 0)) || ((searchDto.CourtroomNumber != null) && l.CourtroomNumber == searchDto.CourtroomNumber))
                                                        && (((searchDto.ProcessIdentifier == null) || (searchDto.ProcessIdentifier == "")) || ((searchDto.ProcessIdentifier != null) && l.ProcessIdentifier == searchDto.ProcessIdentifier))
                                                        && (((searchDto.InstitutionType == null) || (searchDto.InstitutionType == 0)) || ((searchDto.InstitutionType != null) && l.InstitutionType == searchDto.InstitutionType))
                                                        && (((searchDto.Judge == null) || (searchDto.Judge == 0)) || ((searchDto.Judge != null) && l.Judge == searchDto.Judge))
                                                        && (((searchDto.LocationId == null) || (searchDto.LocationId == 0)) || ((searchDto.LocationId != null) && l.LocationId == searchDto.LocationId))
                                                        && (((searchDto.FromDateAndTime == null) || (searchDto.ToDateAndTime == null)) || ((searchDto.FromDateAndTime != null) && (l.DateAndTime >= searchDto.FromDateAndTime && l.DateAndTime <= searchDto.ToDateAndTime)))
                                                        && ((searchDto.UsersIds == null) || ((searchDto.UsersIds.Count() > 0) && litigationsIds.Contains(l.Id)))
                                                        && (((searchDto.ProcessType == null) || (searchDto.ProcessType == 0)) || ((searchDto.ProcessType != null) && l.Defendant == searchDto.ProcessType)))
                .Include(l => l.Location)
                .Include(l => l.ProcessTypeNavigation)
                .Include(l => l.JudgeNavigation)
                .Include(l => l.DefendantNavigation)
                .Include(l => l.ProsecutorNavigation)
                .Include(l => l.LitigationUsers)
                .ToListAsync();

            var litigationsDtos = litigations.Select(l => mapper.Map<LitigationDto>(l)).ToList();

            return litigationsDtos;
        }

        public async Task<List<LitigationDto>> GetLitigationsForUser(string userId)
        {
            var litigationsUsers = dbContext.LitigationUser.Where(lu => lu.UserId == userId).AsEnumerable();
            var litigationsIds = litigationsUsers.Select(lu => lu.LitigationId);

            var litigations = await dbContext.Litigation.Where(l => litigationsIds.Contains(l.Id))
                .Include(l => l.Location)
                .Include(l => l.ProcessTypeNavigation)
                .Include(l => l.JudgeNavigation)
                .Include(l => l.DefendantNavigation)
                .Include(l => l.ProsecutorNavigation)
                .Include(l => l.LitigationUsers)
                .ToListAsync();
            
            var litigationsDtos = litigations.Select(l => mapper.Map<LitigationDto>(l)).ToList();

            return litigationsDtos;
        }

        public async Task<List<LitigationDto>> OrderByDateAscending(bool isAscending, string userId = null)
        {
            List<LitigationDto> litigationDtos = new List<LitigationDto>();

            if (userId == null)
            {
                litigationDtos = await dbContext
                    .Litigation
                    .Include(l => l.Location)
                    .Include(l => l.ProcessTypeNavigation)
                    .Include(l => l.JudgeNavigation)
                    .Include(l => l.DefendantNavigation)
                    .Include(l => l.ProsecutorNavigation)
                    .Include(l => l.LitigationUsers)
                    .Select(l => mapper.Map<LitigationDto>(l))
                    .ToListAsync();
            }
            else
            {
                var litigationsUsers = dbContext.LitigationUser.Where(lu => lu.UserId == userId).AsEnumerable();
                var litigationsIds = litigationsUsers.Select(lu => lu.LitigationId);

                litigationDtos = await dbContext.Litigation.Where(l => litigationsIds.Contains(l.Id))
                    .Include(l => l.Location)
                    .Include(l => l.ProcessTypeNavigation)
                    .Include(l => l.JudgeNavigation)
                    .Include(l => l.DefendantNavigation)
                    .Include(l => l.ProsecutorNavigation)
                    .Include(l => l.LitigationUsers)
                    .Select(l => mapper.Map<LitigationDto>(l))
                    .ToListAsync();
            }

            List<LitigationDto> orderedLitigations = new List<LitigationDto>();

            if (isAscending)
            {
                orderedLitigations = litigationDtos.OrderBy(l => l.DateAndTime).ToList();
            }
            else
            {
                orderedLitigations = litigationDtos.OrderByDescending(l => l.DateAndTime).ToList();
            }

            return orderedLitigations;
        }
    }
}
