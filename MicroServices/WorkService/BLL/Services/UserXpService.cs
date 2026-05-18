using BLL.DTO.UserXp;
using BLL.Services.Interfaces;
using DAL.Unit_of_work;

namespace BLL.Services
{
    public class UserXpService : IUserXpService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserXpService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserXpReadDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var entries = await unitOfWork.UserXp.GetByUserIdAsync(userId, cancellationToken);
            return entries.Select(e => new UserXpReadDto
            {
                Id = e.Id,
                UserId = e.UserId,
                TaskId = e.TaskId,
                TaskName = e.TaskName,
                XpAmount = e.XpAmount,
                EarnedAt = e.EarnedAt,
            });
        }

        public async Task<int> GetTotalXpByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await unitOfWork.UserXp.GetTotalXpByUserIdAsync(userId, cancellationToken);
        }

        public async Task AdjustXpAsync(UserXpAdjustDto dto, CancellationToken cancellationToken = default)
        {
            var entry = new Domain.Entities.UserXp
            {
                UserId = dto.UserId,
                TaskId = Guid.Empty,
                XpAmount = dto.XpAmount,
            };
            await unitOfWork.UserXp.AddAsync(entry, cancellationToken);
        }

        public async Task<PagedLeaderboardDto> GetAllUsersAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            int totalCount = await unitOfWork.UserXp.GetAllUsersCountAsync(cancellationToken);
            var entries = await unitOfWork.UserXp.GetAllUsersWithXpAsync(pageNumber, pageSize, cancellationToken);
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int offset = (pageNumber - 1) * pageSize;

            var items = entries.Select((e, i) => new UserXpLeaderboardDto
            {
                UserId = e.UserId,
                TotalXp = e.TotalXp,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Rank = offset + i + 1,
            }).ToList();

            return new PagedLeaderboardDto
            {
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages == 0 ? 1 : totalPages,
                CurrentPage = pageNumber,
            };
        }

        public async Task RenameUserAsync(Guid userId, string firstName, string lastName, CancellationToken cancellationToken = default)
        {
            await unitOfWork.UserXp.RenameUserAsync(userId, firstName, lastName, cancellationToken);
        }

        public async Task<PagedLeaderboardDto> GetLeaderboardAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            int totalCount = await unitOfWork.UserXp.GetLeaderboardCountAsync(cancellationToken);
            var entries = await unitOfWork.UserXp.GetLeaderboardAsync(pageNumber, pageSize, cancellationToken);
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int offset = (pageNumber - 1) * pageSize;

            var items = entries.Select((e, i) => new UserXpLeaderboardDto
            {
                UserId = e.UserId,
                TotalXp = e.TotalXp,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Rank = offset + i + 1,
            }).ToList();

            return new PagedLeaderboardDto
            {
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages == 0 ? 1 : totalPages,
                CurrentPage = pageNumber,
            };
        }
    }
}
