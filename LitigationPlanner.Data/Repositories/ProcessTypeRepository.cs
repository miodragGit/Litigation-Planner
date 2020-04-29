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
    public class ProcessTypeRepository : IProcessTypeRepository
    {
        private readonly LitigationPlannerDBContext dbContext;
        private readonly IMapper mapper;

        public ProcessTypeRepository(LitigationPlannerDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<int> CreateAsync(ProcessTypeDto processTypeDto)
        {
            var processType = mapper.Map<ProcessType>(processTypeDto);

            await dbContext.ProcessType.AddAsync(processType);
            await dbContext.SaveChangesAsync();

            return processType.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var processType = await dbContext.ProcessType.FirstOrDefaultAsync(pt => pt.Id == id);

            dbContext.ProcessType.Remove(processType);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<ProcessTypeDto> GetAsync(int id)
        {
            var processType = await dbContext.ProcessType.FirstOrDefaultAsync(pt => pt.Id == id);
            var processTypeDto = mapper.Map<ProcessTypeDto>(processType);

            return processTypeDto;
        }

        public async Task<List<ProcessTypeDto>> GetAsync()
        {
            var query = dbContext.ProcessType
                .Select(pt => mapper.Map<ProcessTypeDto>(pt));

            var processTypeDtos = await query.ToListAsync();

            return processTypeDtos;
        }

        public async Task<bool> UpdateAsync(ProcessTypeDto processTypeDto)
        {
            var processType = await dbContext.ProcessType.FirstOrDefaultAsync(pt => pt.Id == processTypeDto.Id);

            processType.Title = processTypeDto.Title;
     
            dbContext.ProcessType.Update(processType);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<ProcessTypeDto>> SearchLitigationAsync(ProcessTypeSearchDto searchDto)
        {
            var processTypes = await dbContext.ProcessType
                                  .Where(l => (searchDto.Title == null) || ((searchDto.Title != null) && l.Title == searchDto.Title))
                                  .ToListAsync();

            var processTypesDtos = processTypes.Select(pt => mapper.Map<ProcessTypeDto>(pt)).ToList();

            return processTypesDtos;
        }
    }
}
