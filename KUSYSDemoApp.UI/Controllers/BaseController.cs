using AutoMapper;
using KUSYSDemoApp.Domain.DBModels;
using KUSYSDemoApp.Service;
using KUSYSDemoApp.UI.Attributes;
using KUSYSDemoApp.UI.Management;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace KUSYSDemoApp.UI.Controllers
{
    [ServiceFilter(typeof(LoginAttribute))]
    public class BaseController : Controller
    {
        public IControllerManager? ControllerManager { get; set; }
        private ISessionManager Session => ControllerManager!.Session;
        private IMemoryCache MemoryCache => ControllerManager!.MemoryCache;
        private IUserService UserService => ControllerManager!.UserService;
        protected IMapper Mapper => ControllerManager!.Mapper;

        /// <summary>
        /// Mevcut Kullanıcının Id sini İfade Eder
        /// </summary>
        protected Guid CurrentUserId => Session.CurrentSession.UserId;

        /// <summary>
        /// Mevcut Kullanıcının Öprenci Id sini İfade Eder
        /// </summary>
        protected Guid? CurrentStudentId => Session.CurrentSession.StudentId;

        /// <summary>
        /// Mevcut Kullanıcının Admin Olma Durumunu İfade Eder
        /// </summary>
        protected bool IsAdmin 
        {
            get
            {
                string key = "IsAdmin";
                Guid userId = CurrentUserId;
                bool isAdmin = false;
                var userAdminStatus = new { userId, isAdmin };

                if (MemoryCache.TryGetValue(key, out userAdminStatus))
                {
                    if (userAdminStatus!.userId == userId) return userAdminStatus.isAdmin;
                }

                User user = UserService.GetUser(userId);

                if (user != null)
                {
                    isAdmin = user.IsAdmin;
                    userAdminStatus = new { userId, isAdmin };
                }

                MemoryCacheEntryOptions options = new()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal
                };

                MemoryCache.Set(key, userAdminStatus, options);                
                return userAdminStatus!.isAdmin;
            }
        }
    }
}