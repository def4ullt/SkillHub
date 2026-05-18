namespace BLL.DTO.UserXp
{
    public class UserXpLeaderboardDto
    {
        public Guid UserId { get; set; }
        public int TotalXp { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Rank { get; set; }
    }

    public class PagedLeaderboardDto
    {
        public List<UserXpLeaderboardDto> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
