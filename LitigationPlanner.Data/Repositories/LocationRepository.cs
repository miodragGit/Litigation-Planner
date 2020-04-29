using AutoMapper;
using LitigationPlanner.Data.DbContexts;
using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Data.Models.Entities;
using LitigationPlanner.Data.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitigationPlanner.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly LitigationPlannerDBContext dbContext;
        private readonly IMapper mapper;

        public LocationRepository(LitigationPlannerDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<int> CreateAsync(LocationDto locationDto)
        {
            var location = mapper.Map<Location>(locationDto);

            await dbContext.Location.AddAsync(location);
            await dbContext.SaveChangesAsync();

            return location.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var location = await dbContext.Location.FirstOrDefaultAsync(l => l.Id == id);

            dbContext.Location.Remove(location);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<LocationDto> GetAsync(int id)
        {
            var location = await dbContext.Location.FirstOrDefaultAsync(l => l.Id == id);
            var locationDto = mapper.Map<LocationDto>(location);

            return locationDto;
        }

        public async Task<List<LocationDto>> GetAsync()
        {
            var query = dbContext.Location
                .Select(l => mapper.Map<LocationDto>(l));

            var locationDtos = await query.ToListAsync();

            return locationDtos;
        }

        public async Task<bool> UpdateAsync(LocationDto locationDto)
        {
            var location = await dbContext.Location.FirstOrDefaultAsync(l => l.Id == locationDto.Id);

            location.Title = locationDto.Title;
            
            dbContext.Location.Update(location);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<LocationDto>> SearchLocatonsAsync(LocationSearchDto searchDto)
        {
            var locations = await dbContext.Location
                                  .Where(l => (searchDto.Title == null) || ((searchDto.Title != null) && l.Title == searchDto.Title))
                                  .ToListAsync();

            var locationsDtos = locations.Select(l => mapper.Map<LocationDto>(l)).ToList();

            return locationsDtos;
        }
    }
}
