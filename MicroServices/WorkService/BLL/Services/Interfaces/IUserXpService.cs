using BLL.DTO.UserXp;

namespace BLL.Services.Interfaces
{
    public interface IUserXpService
    {
        Task<IEnumerable<UserXpReadDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<int> GetTotalXpByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<PagedLeaderboardDto> GetLeaderboardAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task AdjustXpAsync(UserXpAdjustDto dto, CancellationToken cancellationToken = default);
        Task<PagedLeaderboardDto> GetAllUsersAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task RenameUserAsync(Guid userId, string firstName, string lastName, CancellationToken cancellationToken = default);
    }
}
