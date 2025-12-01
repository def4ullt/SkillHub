using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO.Tag;
using BLL.Helpers;
using BLL.Services.Interfaces;
using DAL.Unit_of_work;
using Domain.Entities;

namespace BLL.Services
{
    public class TagService : ITagService
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<TagReadDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<Tag> tags = await unitOfWork.Tags.GetAllAsync();
            List<Tag> tagList = tags.ToList();
            return mapper.Map<List<TagReadDto>>(tagList);
        }

        public async Task<TagReadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Tag? tag = await unitOfWork.Tags.GetByIdAsync(id);
            if (tag == null)
            {
                throw new NotFoundException("Tag not found");
            }

            return mapper.Map<TagReadDto>(tag);
        }

        public async Task<TagReadDto> CreateAsync(TagCreateDto dto, CancellationToken cancellationToken = default)
        {
            if (await unitOfWork.Tags.ExistsByNameAsync(dto.Name, null, cancellationToken))
            {
                throw new DuplicateNameException($"Tag with name '{dto.Name}' already exists");
            }

            Tag tag = mapper.Map<Tag>(dto);
            await unitOfWork.Tags.AddAsync(tag);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<TagReadDto>(tag);
        }

        public async Task<TagReadDto> UpdateAsync(Guid id, TagUpdateDto dto, CancellationToken cancellationToken = default)
        {
            Tag? existingTag = await unitOfWork.Tags.GetByIdAsync(id);
            if (existingTag == null)
            {
                throw new NotFoundException("Tag not found");
            }
            if (await unitOfWork.Tags.ExistsByNameAsync(dto.Name, id, cancellationToken))
            {
                throw new DuplicateNameException($"Tag with name '{dto.Name}' already exists");
            }

            mapper.Map(dto, existingTag);
            await unitOfWork.Tags.UpdateAsync(existingTag);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<TagReadDto>(existingTag);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Tag? tag = await unitOfWork.Tags.GetByIdAsync(id);
            if (tag == null)
            {
                throw new NotFoundException("Tag not found");
            }

            await unitOfWork.Tags.DeleteAsync(tag);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
