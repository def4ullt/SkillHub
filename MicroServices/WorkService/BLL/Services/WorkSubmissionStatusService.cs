using AutoMapper;
using BLL.DTO.WorkSubmissionStatus;
using BLL.Helpers;
using BLL.Services.Interfaces;
using DAL.Unit_of_work;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class WorkSubmissionStatusService : IWorkSubmissionStatusService
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public WorkSubmissionStatusService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<WorkSubmissionStatusReadDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<WorkSubmissionStatus> statuses = await unitOfWork.WorkSubmissionStatuses.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<WorkSubmissionStatusReadDto>>(statuses);
        }

        public async Task<WorkSubmissionStatusReadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            WorkSubmissionStatus? entity = await unitOfWork.WorkSubmissionStatuses.GetByIdAsync(id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(WorkSubmissionStatus), id);

            return mapper.Map<WorkSubmissionStatusReadDto>(entity);
        }

        public async Task<WorkSubmissionStatusReadDto> CreateAsync(WorkSubmissionStatusCreateDto dto, CancellationToken cancellationToken = default)
        {
            bool isUnique = await unitOfWork.WorkSubmissionStatuses.IsNameUniqueAsync(dto.Name, null, cancellationToken);
            if (!isUnique) throw new DuplicateNameException(nameof(WorkSubmissionStatus), dto.Name);

            WorkSubmissionStatus entity = mapper.Map<WorkSubmissionStatus>(dto);
            await unitOfWork.WorkSubmissionStatuses.AddAsync(entity, cancellationToken);
            await unitOfWork.CommitAsync();

            return mapper.Map<WorkSubmissionStatusReadDto>(entity);
        }

        public async Task<WorkSubmissionStatusReadDto> UpdateAsync(Guid id, WorkSubmissionStatusUpdateDto dto, CancellationToken cancellationToken = default)
        {
            WorkSubmissionStatus? entity = await unitOfWork.WorkSubmissionStatuses.GetByIdAsync(id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(WorkSubmissionStatus), id);

            bool isUnique = await unitOfWork.WorkSubmissionStatuses.IsNameUniqueAsync(dto.Name, idToExclude: id, cancellationToken);
            if (!isUnique) throw new DuplicateNameException(nameof(WorkSubmissionStatus), dto.Name);

            mapper.Map(dto, entity);

            WorkSubmissionStatus? updated = await unitOfWork.WorkSubmissionStatuses.UpdateAsync(entity, cancellationToken);
            await unitOfWork.CommitAsync();

            return mapper.Map<WorkSubmissionStatusReadDto>(updated!);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            WorkSubmissionStatus? entity = await unitOfWork.WorkSubmissionStatuses.GetByIdAsync(id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(WorkSubmissionStatus), id);

            await unitOfWork.WorkSubmissionStatuses.DeleteAsync(id, cancellationToken);
            await unitOfWork.CommitAsync();
        }
    }
}