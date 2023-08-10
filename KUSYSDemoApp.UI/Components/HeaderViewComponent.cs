using KUSYSDemoApp.UI.Management;
using Microsoft.AspNetCore.Mvc;

namespace KUSYSDemoApp.UI.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        readonly ISessionManager _sessionManager;

        public HeaderViewComponent(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        /// <summary>
        /// Üst Menü (Header) Bilgilerini Elde Eder
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            ViewBag.UserName = _sessionManager.CurrentSession.UserName;
            ViewBag.UserSurname = _sessionManager.CurrentSession.UserSurname;
            ViewBag.UserNameFirstLetter = _sessionManager.CurrentSession.UserName![0];
            return View();
        }
    }
}