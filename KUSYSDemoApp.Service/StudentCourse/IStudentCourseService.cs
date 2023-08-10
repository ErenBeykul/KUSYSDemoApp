using KUSYSDemoApp.Domain.DBModels;
using KUSYSDemoApp.Domain.DTO;

namespace KUSYSDemoApp.Service
{
    public interface IStudentCourseService
    {        
        /// <summary>
        /// Öğrenci Ve Ders Listesini Elde Eder
        /// </summary>
        /// <returns></returns>
        List<StudentCourseData> GetList();

        /// <summary>
        /// Belli Bir Öğrencinin Ders Listesini Elde Eder
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        List<StudentCourseData> GetList(Guid studentId);

        /// <summary>
        /// Ders Listesini Elde Eder
        /// </summary>
        /// <returns></returns>
        List<Course> GetCourses();

        /// <summary>
        /// Belli Bir Öğrenciye Ait Ders Listesini Elde Eder
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        List<string> GetCourses(Guid studentId);

        /// <summary>
        /// Belli Bir Öğrenci ve Ders Listesini Elde Eder
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        StudentCourseData GetStudentAndCourses(Guid studentId);

        /// <summary>
        /// Belli Bir Öğrenciye Ait Ders Listesini Kaydeder
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseIds"></param>
        void Save(Guid studentId, List<string> courseIds);

        /// <summary>
        /// Belli Bir Öğrenci ve Ders Listesini Siler
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Result Delete(List<Guid> ids);
    }
}