namespace KUSYSDemoApp.UI.Management
{
    public class SessionInfo
    {
        public Guid UserId { get; set; }
        public Guid SessionId { get; set; }
        public Guid? StudentId { get; set; }
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }
    }
}