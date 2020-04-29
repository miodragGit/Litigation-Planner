using LitigationPlanner.Data.Models.DataTransferObjects;
using LitigationPlanner.Data.Models.DataTransferObjects.Search;
using LitigationPlanner.Data.WorkUnitsInterfaces;
using LitigationPlanner.Lib.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitigationPlanner.Lib.Services
{
    public class ProcessTypeService : IProcessTypeService
    {
        private readonly ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork;

        public ProcessTypeService(ILitigationPlannerUnitOfWork litigationPlannerUnitOfWork)
        {
            this.litigationPlannerUnitOfWork = litigationPlannerUnitOfWork;
        }

        public async Task<int> CreateAsync(ProcessTypeDto processTypeDto)
        {
            var processTypeId = await litigationPlannerUnitOfWork.ProcessTypeRepository.CreateAsync(processTypeDto);

            return processTypeId;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var isDeleted = await litigationPlannerUnitOfWork.ProcessTypeRepository.DeleteAsync(id);

            return isDeleted;
        }

        public async Task<ProcessTypeDto> GetAsync(int id)
        {
            var processTypeDto = await litigationPlannerUnitOfWork.ProcessTypeRepository.GetAsync(id);

            return processTypeDto;
        }

        public async Task<List<ProcessTypeDto>> GetAsync()
        {
            var processTypeDtos = await litigationPlannerUnitOfWork.ProcessTypeRepository.GetAsync();

            return processTypeDtos;
        }

        public async Task<bool> UpdateAsync(ProcessTypeDto processTypeDto)
        {
            var isUpdated = await litigationPlannerUnitOfWork.ProcessTypeRepository.UpdateAsync(processTypeDto);

            return isUpdated;
        }

        public async Task<List<ProcessTypeDto>> SearchProcessTypesAsync(ProcessTypeSearchDto searchDto)
        {
            var processTypes = await litigationPlannerUnitOfWork.ProcessTypeRepository.SearchLitigationAsync(searchDto);

            return processTypes;
        }
    }
}
