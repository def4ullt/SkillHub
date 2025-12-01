using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO.DeliveryMethod;
using BLL.Helpers;
using BLL.Services.Interfaces;
using DAL.Unit_of_work;
using Domain.Entities;

namespace BLL.Services
{
    public class SubmissionDeliveryMethodService : ISubmissionDeliveryMethodService
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public SubmissionDeliveryMethodService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<SubmissionDeliveryMethodReadDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<SubmissionDeliveryMethod> entities = await unitOfWork.SubmissionDeliveryMethods.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<SubmissionDeliveryMethodReadDto>>(entities);
        }

        public async Task<SubmissionDeliveryMethodReadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            SubmissionDeliveryMethod? entity = await unitOfWork.SubmissionDeliveryMethods.GetByIdAsync(id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(SubmissionDeliveryMethod), id);

            return mapper.Map<SubmissionDeliveryMethodReadDto>(entity);
        }

        public async Task<SubmissionDeliveryMethodReadDto> CreateAsync(SubmissionDeliveryMethodCreateDto dto, CancellationToken cancellationToken = default)
        {
            bool isUnique = await unitOfWork.SubmissionDeliveryMethods.IsNameUniqueAsync(dto.Name, null, cancellationToken);
            if (!isUnique) throw new DuplicateNameException(nameof(SubmissionDeliveryMethod), dto.Name);

            SubmissionDeliveryMethod entity = mapper.Map<SubmissionDeliveryMethod>(dto);

            Guid id = await unitOfWork.SubmissionDeliveryMethods.AddAsync(entity, cancellationToken);
            await unitOfWork.CommitAsync();

            return mapper.Map<SubmissionDeliveryMethodReadDto>(entity);
        }

        public async Task<SubmissionDeliveryMethodReadDto> UpdateAsync(Guid id, SubmissionDeliveryMethodUpdateDto dto, CancellationToken cancellationToken = default)
        {
            SubmissionDeliveryMethod? entity = await unitOfWork.SubmissionDeliveryMethods.GetByIdAsync(id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(SubmissionDeliveryMethod), id);

            bool isUnique = await unitOfWork.SubmissionDeliveryMethods.IsNameUniqueAsync(dto.Name, idToExclude: id, cancellationToken: cancellationToken);
            if (!isUnique) throw new DuplicateNameException(nameof(SubmissionDeliveryMethod), dto.Name);

            mapper.Map(dto, entity);

            SubmissionDeliveryMethod? updated = await unitOfWork.SubmissionDeliveryMethods.UpdateAsync(entity, cancellationToken);
            await unitOfWork.CommitAsync();

            return mapper.Map<SubmissionDeliveryMethodReadDto>(updated!);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            SubmissionDeliveryMethod? entity = await unitOfWork.SubmissionDeliveryMethods.GetByIdAsync(id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(SubmissionDeliveryMethod), id);

            await unitOfWork.SubmissionDeliveryMethods.DeleteAsync(id, cancellationToken);
            await unitOfWork.CommitAsync();
        }
    }
}
