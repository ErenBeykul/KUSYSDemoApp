using KUSYSDemoApp.Domain.DBModels;
using KUSYSDemoApp.Domain.Extensions;
using KUSYSDemoApp.Service;
using KUSYSDemoApp.UI.Management;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace KUSYSDemoApp.UI.Attributes
{
    public class LoginAttribute : ActionFilterAttribute
    {
        readonly IUserService _userService;
        readonly IMemoryCache _memoryCache;
        readonly ISessionManager _sessionManager;

        public LoginAttribute(IUserService userService, IMemoryCache memoryCache, ISessionManager sessionManager)
        {
            _userService = userService;
            _memoryCache = memoryCache;
            _sessionManager = sessionManager;
        }

        /// <summary>
        /// Cookie ve Session Kontrollerini Yapar
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            SessionInfo currentSession = _sessionManager.CurrentSession;
            if (context.HttpContext.Request.Cookies["SessionId"] == null || currentSession == null)
            {
                context.Result = RedirectToLogin();
            }
            else if (context.HttpContext.Request.Cookies["SessionId"] != null)
            {
                Guid userId = Guid.Empty;
                Guid sessionId = Guid.Empty;
                Guid sessionIdInCookie = Guid.Parse(context.HttpContext.Request.Cookies["SessionId"]!.ODecode());

                if (currentSession != null)
                {
                    userId = currentSession.UserId;
                    sessionId = currentSession.SessionId;
                }

                if (sessionIdInCookie != sessionId)
                {
                    context.Result = RedirectToLogin();
                }
                else
                {
                    Session dataSession = GetDataSession(userId, sessionId);

                    if (dataSession == null)
                    {
                        context.Result = RedirectToLogin();
                    }
                    else
                    {
                        string userAgent = context.HttpContext.Request.Headers["User-Agent"];
                        string encodedUserAgent = userId + "###" + userAgent;
                        string oEncodedUserAgent = encodedUserAgent.OEncode();

                        if (dataSession.Id != sessionId || dataSession.UserAgent != oEncodedUserAgent)
                        {
                            context.Result = RedirectToLogin();
                        }
                        else
                        {
                            #region Başarılı Kontrol Sonucunda Oturum ve Çerezinin Ömrü Yarım Saat Daha Uzatılıyor
                            CookieOptions cookie = new CookieOptions
                            {
                                Expires = DateTime.Now.AddMinutes(30)
                            };

                            _sessionManager.CurrentSession = currentSession!;
                            #endregion
                        }
                    }
                }
            }

            base.OnActionExecuting(context);
        }


        /// <summary>
        /// Chache deki veya DB deki Mevcut Oturumu Elde Eder
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private Session GetDataSession(Guid userId, Guid sessionId)
        {
            string key = "DataSession";////

            if (_memoryCache.TryGetValue(key, out Session dataSession))
            {
                if (dataSession.UserId == userId && dataSession.Id == sessionId) return dataSession;
            }

            dataSession = _userService.GetSession(userId);

            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            };

            _memoryCache.Set(key, dataSession, options);

            return dataSession;
        }


        /// <summary>
        /// Login Sayfasına Yönlendirir
        /// </summary>
        /// <returns></returns>
        private RedirectToRouteResult RedirectToLogin()
        {
            return new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
        }
    }
}