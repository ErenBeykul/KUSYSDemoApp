namespace KUSYSDemoApp.Domain.DBModels
{
    public class Session
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? LoginTime { get; set; }
        public string UserAgent { get; set; } = string.Empty;

        public virtual User? User { get; set; }
    }
}