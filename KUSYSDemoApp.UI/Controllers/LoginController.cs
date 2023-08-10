using KUSYSDemoApp.Domain.Constants;
using KUSYSDemoApp.Domain.DBModels;
using KUSYSDemoApp.Domain.DTO;
using KUSYSDemoApp.Domain.Enums;
using KUSYSDemoApp.Domain.Extensions;
using KUSYSDemoApp.Service;
using KUSYSDemoApp.UI.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYSDemoApp.UI.Controllers
{
    public class LoginController : Controller
    {
        readonly IUserService _userService;
        readonly ISessionManager _sessionManager;

        public LoginController(IUserService userService, ISessionManager sessionManager)
        {
            _userService = userService;
            _sessionManager = sessionManager;
        }

        /// <summary>
        /// Kullanıcı Giriş (Login) Formu
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Kullanıcı Giriş (Login) İşlemleri
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Index(string username, string password)
        {
            Result result = new();

            #region Zorunlu Alan Kontrolleri
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                result.Type = ResultName.Warning.ToLowerString();
                result.Message = ResultMessages.EmptyUsernameOrPassword;

                return Json(result);
            }
            #endregion

            try
            {
                #region Giriş (Login) İşlemleri Gerçekleştiriliyor
                User user = _userService.GetUser(username, password);

                if (user == null)
                {
                    result.Type = ResultName.Warning.ToLowerString();
                    result.Message = ResultMessages.WrongUsernameOrPassword;

                    return Json(result);
                }

                #region Sisteme Giriş Yapan Kullanıcı İçin Session Bilgileri Set Ediliyor ve DB ye Kaydediliyor
                string userAgent = Request.Headers["User-Agent"];
                string encodedUserAgent = user.Id + "###" + userAgent;
                string oEncodedUserAgent = encodedUserAgent.OEncode();

                Session session = new()
                {
                    UserId = user.Id,
                    UserAgent = oEncodedUserAgent
                };

                Result sessionResult = _userService.Save(session);

                if (!sessionResult.IsSuccess)
                {
                    result.Type = ResultName.Error.ToLowerString();
                    result.Message = ResultMessages.Error;

                    return Json(result);
                }
                #endregion

                #region Eğer Kullanıcının Oturum (Session) Bilgilerinin Sistemde Kaydı Varsa, Başarılı Giriş (Login) İşlemleri Gerçekleştiriliyor
                Session dataSession = _userService.GetSession(user.Id);

                if (dataSession == null)
                {
                    result.Type = ResultName.Error.ToLowerString();
                    result.Message = ResultMessages.Error;

                    return Json(result);
                }

                #region Mevcut Kullanıcı İçin Oturum (Session) Bilgileri Set Ediliyor ve Session Oluşturuluyor
                SessionInfo currentSession = new()
                {
                    UserId = user.Id,
                    UserName = user.Name,
                    UserSurname = user.Surname,
                    StudentId = user.StudentId,
                    SessionId = dataSession.Id
                };

                _sessionManager.CurrentSession = currentSession;
                #endregion

                #region Mevcut Oturum (Session) İçin Çerez Oluşturuluyor ve Oturum Id si (SessionId) Çereze (Cookie) Yazılıyor
                CookieOptions cookie = new()
                {
                    Expires = DateTime.Now.AddMinutes(30)
                };

                Response.Cookies.Append("SessionId", dataSession.Id.ToString().OEncode());
                #endregion

                #region Mevcut Kullanıcının Oturum (Session) Id si ve Son Giriş Tarihleri Kaydediliyor
                user.LastLogin = DateTime.Now;
                user.LastSessionId = dataSession.Id;
                Result userSavingResult = _userService.UpdateLoginInfo(user);

                if (!userSavingResult.IsSuccess)
                {
                    result.Type = ResultName.Error.ToLowerString();
                    result.Message = ResultMessages.Error;

                    return Json(result);
                }
                #endregion

                #region Kullanıcıya Başarılı Giriş Mesajı Veriliyor
                result.IsSuccess = true;
                result.Type = ResultName.Success.ToLowerString();
                result.Message = ResultMessages.LoginSuccess;
                #endregion

                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return Json(result);
        }

        /// <summary>
        /// Kullanıcı Çıkış (Logout) İşlemleri
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            try
            {
                #region Mevcut Oturum İçin Oluşturulan Çerez (Cookie) Siliniyor
                if (Request.Cookies["SessionId"] != null) Response.Cookies.Delete("SessionId");

                CookieOptions cookie = new()
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                #endregion

                #region Mevcut Oturum DB den ve Tarayıcıdan Siliniyor
                if (_sessionManager.CurrentSession != null)
                {
                    Guid sessionId = _sessionManager.CurrentSession.SessionId;
                    HttpContext.Session.Clear();
                    _userService.Delete(sessionId);
                }
                #endregion

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return View("~/Views/System/Error.cshtml");
            }
        }
    }
}