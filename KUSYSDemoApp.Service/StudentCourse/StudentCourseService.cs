using KUSYSDemoApp.DataAccess;
using KUSYSDemoApp.Domain.Constants;
using KUSYSDemoApp.Domain.DBModels;
using KUSYSDemoApp.Domain.DTO;
using KUSYSDemoApp.Domain.Enums;
using KUSYSDemoApp.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace KUSYSDemoApp.Service
{
    public class StudentCourseService : IStudentCourseService
    {
        readonly IKUSYSDemoAppContext _context;

        public StudentCourseService(IKUSYSDemoAppContext context)
        {
            _context = context;
        }

        public List<StudentCourseData> GetList()
        {
            return _context.StudentCourses.Include(x => x.Course)
                                            .Include(x => x.Student)
                                            .Select(x => new StudentCourseData
                                            {
                                                Id = x.Id,
                                                CourseCode = x.CourseId,
                                                CourseName = x.Course!.Name,
                                                CreateDate = x.CreateDate.ToString("dd.MM.yyyy"),
                                                Student = x.Student!.Name + " " + x.Student.Surname

                                            }).ToList();
        }

        public List<StudentCourseData> GetList(Guid studentId)
        {
            return _context.StudentCourses.Include(x => x.Course)
                                            .Include(x => x.Student)
                                            .Where(x => x.StudentId == studentId)
                                            .Select(x => new StudentCourseData
                                            {
                                                Id = x.Id,
                                                CourseCode = x.CourseId,
                                                CourseName = x.Course!.Name,
                                                CreateDate = x.CreateDate.ToString("dd.MM.yyyy"),
                                                Student = x.Student!.Name + " " + x.Student.Surname

                                            }).ToList();
        }

        public List<Course> GetCourses()
        {
            return _context.Courses.Where(x => !x.IsDeleted).ToList();
        }
        
        public List<string> GetCourses(Guid studentId)
        {
            return _context.StudentCourses.Where(x => x.StudentId == studentId).Select(x => x.CourseId).ToList();
        }

        public StudentCourseData GetStudentAndCourses(Guid studentId)
        {
            return _context.Students.Include(x => x.StudentCourses)
                                    .Where(x => x.Id == studentId)
                                    .Select(x => new StudentCourseData
                                    {
                                        Student = x.Name + " " + x.Surname,
                                        CourseIds = x.StudentCourses.Select(x => x.CourseId).ToList()

                                    }).FirstOrDefault()!;
        }

        public void Save(Guid studentId, List<string> courseIds)
        {
            var dataCourses = _context.StudentCourses.Where(x => x.StudentId == studentId).AsEnumerable();
            _context.StudentCourses.RemoveRange(dataCourses.Where(x => !courseIds.Contains(x.CourseId)));

            foreach (var item in courseIds)
            {
                StudentCourse dataCourse = dataCourses.FirstOrDefault(x => x.CourseId == item)!;

                if (dataCourse == null)
                {
                    StudentCourse course = new()
                    {
                        Id = Guid.NewGuid(),
                        StudentId = studentId,
                        CourseId = item,
                        CreateDate = DateTime.Now
                    };

                    _context.StudentCourses.Add(course);
                }
            }

            _context.SaveChanges();
        }

        public Result Delete(List<Guid> ids)
        {
            Result result = new();
            _context.StudentCourses.RemoveRange(_context.StudentCourses.Where(x => ids.Contains(x.Id)));
            
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