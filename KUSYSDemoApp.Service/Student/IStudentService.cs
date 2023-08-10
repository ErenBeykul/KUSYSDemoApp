using KUSYSDemoApp.Domain.DBModels;
using KUSYSDemoApp.Domain.DTO;

namespace KUSYSDemoApp.Service
{
    public interface IStudentService
    {
        /// <summary>
        /// Öğrenci Listesini Elde Eder
        /// </summary>
        /// <returns></returns>
        List<Student> GetList();

        /// <summary>
        /// Belli Bir Öğrenciyi Elde Eder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student GetStudent(Guid id);

        /// <summary>
        /// Belli Bir Öğrenciyi Kaydeder
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Result Save(Student student);

        /// <summary>
        /// Belli Bir Öğrenci Listesini Siler
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Result Delete(List<Guid> ids);
    }
}