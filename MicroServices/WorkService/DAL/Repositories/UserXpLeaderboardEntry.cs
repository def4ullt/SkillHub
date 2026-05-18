namespace DAL.Repositories
{
    public class UserXpLeaderboardEntry
    {
        public Guid UserId { get; set; }
        public int TotalXp { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
