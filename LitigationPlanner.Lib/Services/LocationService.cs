using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Data.WorkUnitsInterfaces;
using LitigationPlanner.Lib.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Lib.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork;

        public LocationService(ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork)
        {
            this.litigationPlannerUnitOfWork = litigationPlannerUnitOfWork;
        }

        public async Task<int> CreateAsync(LocationDto locationDto)
        {
            var locationId = await litigationPlannerUnitOfWork.LocationRepository.CreateAsync(locationDto);

            return locationId;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var isDeleted = await litigationPlannerUnitOfWork.LocationRepository.DeleteAsync(id);

            return isDeleted;
        }

        public async Task<LocationDto> GetAsync(int id)
        {
            var locationDto = await litigationPlannerUnitOfWork.LocationRepository.GetAsync(id);

            return locationDto;
        }

        public async Task<List<LocationDto>> GetAsync()
        {
            var locationDtos = await litigationPlannerUnitOfWork.LocationRepository.GetAsync();

            return locationDtos;
        }

        public async Task<bool> UpdateAsync(LocationDto locationDto)
        {
            var isUpdated = await litigationPlannerUnitOfWork.LocationRepository.UpdateAsync(locationDto);

            return isUpdated;
        }

        public async Task<List<LocationDto>> SearchLocationsAsync(LocationSearchDto searchDto)
        {
            var locations = await litigationPlannerUnitOfWork.LocationRepository.SearchLocatonsAsync(searchDto);

            return locations;
        }
    }
}
