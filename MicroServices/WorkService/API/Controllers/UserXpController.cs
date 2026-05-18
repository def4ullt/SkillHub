using BLL.DTO.UserXp;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [ApiController]
    [Route("api/user-xp")]
    public class UserXpController : ControllerBase
    {
        private readonly IUserXpService service;

        public UserXpController(IUserXpService service)
        {
            this.service = service;
        }

        [HttpGet("leaderboard")]
        public async Task<ActionResult<PagedLeaderboardDto>> GetLeaderboard(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await service.GetLeaderboardAsync(pageNumber, pageSize, cancellationToken);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserXpReadDto>>> GetByUser(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var result = await service.GetByUserIdAsync(userId, cancellationToken);
            return Ok(result);
        }

        [HttpGet("user/{userId}/total")]
        public async Task<ActionResult<int>> GetTotalByUser(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            int total = await service.GetTotalXpByUserIdAsync(userId, cancellationToken);
            return Ok(total);
        }

        [HttpGet("users")]
        public async Task<ActionResult<PagedLeaderboardDto>> GetAllUsers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 15,
            CancellationToken cancellationToken = default)
        {
            var result = await service.GetAllUsersAsync(pageNumber, pageSize, cancellationToken);
            return Ok(result);
        }

        [HttpPost("adjust")]
        public async Task<ActionResult> AdjustXp(
            [FromBody] UserXpAdjustDto dto,
            CancellationToken cancellationToken = default)
        {
            await service.AdjustXpAsync(dto, cancellationToken);
            return NoContent();
        }

        [HttpPut("users/{userId}/rename")]
        public async Task<ActionResult> RenameUser(
            Guid userId,
            [FromBody] UserXpRenameDto dto,
            CancellationToken cancellationToken = default)
        {
            await service.RenameUserAsync(userId, dto.FirstName, dto.LastName, cancellationToken);
            return NoContent();
        }
    }
}
