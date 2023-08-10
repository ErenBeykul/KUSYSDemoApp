using AutoMapper;
using KUSYSDemoApp.Service;
using Microsoft.Extensions.Caching.Memory;

namespace KUSYSDemoApp.UI.Management
{
    public class ControllerManager : IControllerManager
    {
        public IMapper Mapper { get; }
        public ISessionManager Session { get; }
        public IMemoryCache MemoryCache { get; }
        public IUserService UserService { get; }

        public ControllerManager(IMapper mapper, ISessionManager session, IMemoryCache memoryCache, IUserService userService)
        {
            Mapper = mapper;
            Session = session;
            MemoryCache = memoryCache;
            UserService = userService;
        }
    }
}