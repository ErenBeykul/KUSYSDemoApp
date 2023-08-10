using AutoMapper;
using KUSYSDemoApp.Service;
using Microsoft.Extensions.Caching.Memory;

namespace KUSYSDemoApp.UI.Management
{
    public interface IControllerManager
    {
        IMapper Mapper { get; }
        ISessionManager Session { get; }
        IMemoryCache MemoryCache { get; }
        IUserService UserService { get; }
    }
}