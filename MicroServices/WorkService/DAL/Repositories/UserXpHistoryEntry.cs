namespace DAL.Repositories
{
    public class UserXpHistoryEntry
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }
        public int XpAmount { get; set; }
        public DateTime EarnedAt { get; set; }
        public string? TaskName { get; set; }
    }
}
