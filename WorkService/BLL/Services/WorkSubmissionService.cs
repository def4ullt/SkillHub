using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO.WorkSubmission;
using BLL.DTO.WorkSubmissionFile;
using BLL.Helpers;
using BLL.Services.Interfaces;
using DAL.Helpers;
using DAL.Unit_of_work;
using Domain.Entities;

namespace BLL.Services
{
    public class WorkSubmissionService : IWorkSubmissionService
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public WorkSubmissionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<WorkSubmissionReadDto> CreateAsync(WorkSubmissionCreateDto dto, CancellationToken cancellationToken = default)
        {
            WorkSubmissionStatus? status = await unitOfWork.WorkSubmissionStatuses.GetByIdAsync(dto.StatusId, cancellationToken);
            if (status == null)
            {
                throw new NotFoundException(nameof(WorkSubmissionStatus), dto.StatusId);
            }

            WorkSubmission? entity = mapper.Map<WorkSubmission>(dto);

            Guid id = await unitOfWork.WorkSubmissions.AddAsync(entity, cancellationToken);

            if (dto.Files != null)
            {
                foreach (var fileDto in dto.Files)
                {
                    WorkSubmissionFile file = mapper.Map<WorkSubmissionFile>(fileDto);
                    file.WorkSubmissionId = id;
                    await unitOfWork.WorkSubmissionFiles.AddAsync(file, cancellationToken);
                }
            }

            await unitOfWork.CommitAsync();

            return mapper.Map<WorkSubmissionReadDto>(entity);
        }

        public async Task<WorkSubmissionDetailDto?> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
        {
            WorkSubmissionDetail? entity = await unitOfWork.WorkSubmissions.GetDetailAsync(id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(WorkSubmission), id);
            }

            return mapper.Map<WorkSubmissionDetailDto>(entity);
        }

        public async Task<WorkSubmissionReadDto> UpdateAsync(Guid id, WorkSubmissionUpdateDto dto, CancellationToken cancellationToken = default)
        {
            WorkSubmission? entity = await unitOfWork.WorkSubmissions.GetByIdAsync(id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(WorkSubmission), id);
            }

            WorkSubmissionStatus? status = await unitOfWork.WorkSubmissionStatuses.GetByIdAsync(dto.StatusId, cancellationToken);
            if (status == null)
            {
                throw new NotFoundException(nameof(WorkSubmissionStatus), dto.StatusId);
            }

            entity.StatusId = dto.StatusId;

            WorkSubmission? updated = await unitOfWork.WorkSubmissions.UpdateAsync(entity, cancellationToken);

            List<WorkSubmissionFile> existingFiles = await unitOfWork.WorkSubmissionFiles.GetBySubmissionIdAsync(entity.Id, cancellationToken);
            foreach (WorkSubmissionFile oldFile in existingFiles)
            {
                await unitOfWork.WorkSubmissionFiles.DeleteAsync(oldFile.Id, cancellationToken);
            }

            if (dto.Files != null)
            {
                foreach (WorkSubmissionFileUpdateDto fileDto in dto.Files)
                {
                    WorkSubmissionFile newFile = mapper.Map<WorkSubmissionFile>(fileDto);
                    newFile.WorkSubmissionId = entity.Id;
                    await unitOfWork.WorkSubmissionFiles.AddAsync(newFile, cancellationToken);
                }
            }

            await unitOfWork.CommitAsync();

            return mapper.Map<WorkSubmissionReadDto>(updated);
        }


        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            WorkSubmission? entity = await unitOfWork.WorkSubmissions.GetByIdAsync(id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(WorkSubmission), id);
            }

            int affected = await unitOfWork.WorkSubmissions.DeleteAsync(id, cancellationToken);
            await unitOfWork.CommitAsync();

            return affected > 0;
        }

        public async Task<PagedList<WorkSubmissionReadDto>> GetPagedAsync(WorkSubmissionQueryParams queryParams, CancellationToken cancellationToken = default)
        {
            PagedList<WorkSubmission> pagedEntities = await unitOfWork.WorkSubmissions.GetPagedAsync(queryParams, cancellationToken);

            List<WorkSubmissionReadDto> pagedDtos = pagedEntities.Items.Select(ws => mapper.Map<WorkSubmissionReadDto>(ws)).ToList();

            return new PagedList<WorkSubmissionReadDto>(
                pagedDtos,
                pagedEntities.TotalCount,
                pagedEntities.CurrentPage,
                pagedEntities.PageSize
            );
        }
    }
}
