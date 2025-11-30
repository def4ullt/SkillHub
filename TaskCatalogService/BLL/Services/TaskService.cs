using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO.Task;
using BLL.Helpers;
using BLL.Services.Interfaces;
using DAL.Unit_of_work;
using Domain.Entities;
using Domain.Query;

namespace BLL.Services
{
    public class TaskService : ITaskService
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<TaskReadDto>> GetPagedTasksAsync(TaskQueryParameters parameters, CancellationToken cancellationToken = default)
        {
            IEnumerable<TaskEntity> tasks = await unitOfWork.Tasks.GetPagedTasksAsync(parameters, cancellationToken);
            List<TaskEntity> taskList = tasks.ToList();
            
            return mapper.Map<List<TaskReadDto>>(taskList);
        }

        public async Task<TaskDetailDto?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            TaskEntity? task = await unitOfWork.Tasks.GetTaskByIdAsync(id, cancellationToken);
            if (task == null)
            {
                throw new NotFoundException($"Task with id {id} not found");
            }
            
            return mapper.Map<TaskDetailDto>(task);
        }

        public async Task<TaskDetailDto> CreateTaskAsync(TaskCreateDto dto, CancellationToken cancellationToken = default)
        {
            if (!await unitOfWork.Technologies.AreTechnologiesValidAsync(dto.TechnologyIds, cancellationToken))
            {
                throw new NotFoundException("Some of the provided Technology IDs do not exist");
            }

            if (!await unitOfWork.Tags.AreTagsValidAsync(dto.TagIds, cancellationToken))
            {
                throw new NotFoundException("Some of the provided Tag IDs do not exist");
            }

            TaskEntity task = mapper.Map<TaskEntity>(dto);

            task.TaskTechnologies.Clear();
            foreach (Guid techId in dto.TechnologyIds)
            {
                task.TaskTechnologies.Add(new TaskTechnology { TechnologyId = techId });
            }

            task.TaskTags.Clear();
            foreach (Guid tagId in dto.TagIds)
            {
                task.TaskTags.Add(new TaskTag { TagId = tagId });
            }

            await unitOfWork.Tasks.AddAsync(task);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<TaskDetailDto>(task);
        }

        public async Task<TaskDetailDto> UpdateTaskAsync(Guid id, TaskUpdateDto dto, CancellationToken cancellationToken = default)
        {
            TaskEntity? existingTask = await unitOfWork.Tasks.GetTaskByIdAsync(id);
            if (existingTask == null)
            {
                throw new NotFoundException($"Task with id {id} not found");
            }

            if (!await unitOfWork.Technologies.AreTechnologiesValidAsync(dto.TechnologyIds, cancellationToken))
            {
                throw new NotFoundException("Some of the provided Technology IDs do not exist");
            }

            if (!await unitOfWork.Tags.AreTagsValidAsync(dto.TagIds, cancellationToken))
            {
                throw new NotFoundException("Some of the provided Tag IDs do not exist");
            }

            mapper.Map(dto, existingTask);

            existingTask.TaskTechnologies.Clear();
            foreach (Guid techId in dto.TechnologyIds)
            {
                existingTask.TaskTechnologies.Add(new TaskTechnology { TechnologyId = techId });
            }

            existingTask.TaskTags.Clear();
            foreach (Guid tagId in dto.TagIds)
            {
                existingTask.TaskTags.Add(new TaskTag { TagId = tagId });
            }

            await unitOfWork.Tasks.UpdateAsync(existingTask);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<TaskDetailDto>(existingTask);
        }

        public async Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default)
        {
            TaskEntity? existingTask = await unitOfWork.Tasks.GetByIdAsync(id);
            if (existingTask == null)
            {
                throw new NotFoundException($"Task with id {id} not found");
            }

            await unitOfWork.Tasks.DeleteAsync(existingTask);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
