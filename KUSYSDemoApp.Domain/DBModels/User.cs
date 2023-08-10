namespace KUSYSDemoApp.Domain.DBModels
{
    public class User
    {
        public Guid Id { get; set; }
        public bool IsAdmin { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? LastSessionId { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public virtual Session? Session { get; set; }
    }
}