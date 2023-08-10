using KUSYSDemoApp.Domain.DBModels;
using KUSYSDemoApp.Domain.DTO;

namespace KUSYSDemoApp.Service
{
    public interface IUserService
    {
        /// <summary>
        /// Belli Bir Kullanıcıyı Elde Eder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUser(Guid id);

        /// <summary>
        /// Belli Bir Kullanıcıyı Kullanıcı Adı ve Şifresiyle Elde Eder
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User GetUser(string username, string password);

        /// <summary>
        /// Belli Bir Kullanıcıya Ait Oturum (Session) Bilgilerini Elde Eder
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Session GetSession(Guid userId);

        /// <summary>
        /// Belli Bir Kullanıcıya Ait Oturum (Session) Bilgilerini Kaydeder
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        Result Save(Session session);

        /// <summary>
        /// Belli Bir Kullanıcının Giriş (Login) Bilgilerini Günceller
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Result UpdateLoginInfo(User user);

        /// <summary>
        /// Belli Bir Kullanıcıya Ait Oturum Bilgilerini Siler
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        Result Delete(Guid sessionId);
    }
}