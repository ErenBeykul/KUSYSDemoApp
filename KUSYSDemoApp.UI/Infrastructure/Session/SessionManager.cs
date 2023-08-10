using System.Text.Json;
using KUSYSDemoApp.Domain.Extensions;

namespace KUSYSDemoApp.UI.Management
{
    public class SessionManager : ISessionManager
    {
        readonly IHttpContextAccessor _httpContextAccessor;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Mevcut Kullanıcının Oturum (Session) Bilgilerini Set Eder
        /// </summary>
        public SessionInfo CurrentSession
        {
            get
            {
                string sessionEncoded = _httpContextAccessor.HttpContext!.Session.GetString("UserSession")!;
                if (sessionEncoded != null) return JsonSerializer.Deserialize<SessionInfo>(sessionEncoded.ODecode())!;
                return default!;
            }

            set
            {
                _httpContextAccessor.HttpContext!.Session.SetString("UserSession", JsonSerializer.Serialize(value).OEncode());
            }
        }
    }
}