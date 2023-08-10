using KUSYSDemoApp.DataAccess;
using KUSYSDemoApp.Domain.Constants;
using KUSYSDemoApp.Domain.DBModels;
using KUSYSDemoApp.Domain.DTO;
using KUSYSDemoApp.Domain.Enums;
using KUSYSDemoApp.Domain.Extensions;

namespace KUSYSDemoApp.Service
{
    public class UserService : IUserService
    {
        readonly IKUSYSDemoAppContext _context;

        public UserService(IKUSYSDemoAppContext context)
        {
            _context = context;
        }

        public User GetUser(Guid id)
        {
            return _context.Users.Find(id)!;
        }

        public User GetUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username && x.Password == password)!;
        }

        public Session GetSession(Guid userId)
        {
            return _context.Sessions.FirstOrDefault(x => x.UserId == userId)!;
        }

        public Result Save(Session session)
        {
            Result result = new();
            Session dataSession = GetSession(session.UserId);

            if (dataSession == null)
            {
                #region Ekleme İşlemleri
                session.Id = Guid.NewGuid();
                session.LoginTime = DateTime.Now;

                _context.Sessions.Add(session);
                #endregion

            }
            else
            {
                #region Düzenleme İşlemleri
                dataSession.LoginTime = DateTime.Now;
                dataSession.UserAgent = session.UserAgent;

                _context.Sessions.Update(dataSession);
                #endregion
            }

            if (_context.SaveChanges() > 0)
            {
                result.IsSuccess = true;
                result.Type = ResultName.Success.ToLowerString();
                result.Message = ResultMessages.Success;

            }
            else
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }        

        public Result UpdateLoginInfo(User user)
        {
            Result result = new();
            User dataUser = GetUser(user.Id);
            dataUser.LastLogin = user.LastLogin;
            dataUser.LastSessionId = user.LastSessionId;

            if (_context.SaveChanges() > 0)
            {
                result.IsSuccess = true;
                result.Type = ResultName.Success.ToLowerString();
                result.Message = ResultMessages.Success;

            }
            else
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }        

        public Result Delete(Guid sessionId)
        {
            Result result = new();
            Session dataSession = _context.Sessions.Find(sessionId)!;
            _context.Sessions.Remove(dataSession);

            if (_context.SaveChanges() > 0)
            {
                result.IsSuccess = true;
                result.Type = ResultName.Success.ToLowerString();
                result.Message = ResultMessages.Success;

            }
            else
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }
    }
}