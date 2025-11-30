using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO.Technology;
using BLL.Helpers;
using BLL.Services.Interfaces;
using DAL.Unit_of_work;
using Domain.Entities;

namespace BLL.Services
{
    public class TechnologyService : ITechnologyService
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public TechnologyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<TechnologyReadDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<Technology> technologies = await unitOfWork.Technologies.GetAllAsync();
            List<Technology> technologyList = technologies.ToList();
            return mapper.Map<List<TechnologyReadDto>>(technologyList);
        }

        public async Task<TechnologyReadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Technology? technology = await unitOfWork.Technologies.GetByIdAsync(id);
            if (technology == null)
            {
                throw new NotFoundException("Technology not found");
            }

            return mapper.Map<TechnologyReadDto>(technology);
        }

        public async Task<TechnologyReadDto> CreateAsync(TechnologyCreateDto dto, CancellationToken cancellationToken = default)
        {
            if (await unitOfWork.Technologies.ExistsByNameAsync(dto.Name, null, cancellationToken))
            {
                throw new DuplicateNameException($"Technology with name '{dto.Name}' already exists");
            }
            Technology technology = mapper.Map<Technology>(dto);
            await unitOfWork.Technologies.AddAsync(technology);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<TechnologyReadDto>(technology);
        }

        public async Task<TechnologyReadDto> UpdateAsync(Guid id, TechnologyUpdateDto dto, CancellationToken cancellationToken = default)
        {
            Technology? existingTechnology = await unitOfWork.Technologies.GetByIdAsync(id);
            if (existingTechnology == null)
            {
                throw new NotFoundException("Technology not found");
            }
            if (await unitOfWork.Technologies.ExistsByNameAsync(dto.Name, id, cancellationToken))
            {
                throw new DuplicateNameException($"Technology with name '{dto.Name}' already exists");
            }

            mapper.Map(dto, existingTechnology);
            await unitOfWork.Technologies.UpdateAsync(existingTechnology);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<TechnologyReadDto>(existingTechnology);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Technology? technology = await unitOfWork.Technologies.GetByIdAsync(id);
            if (technology == null)
            {
                throw new NotFoundException("Technology not found");
            }
            await unitOfWork.Technologies.DeleteAsync(technology);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
