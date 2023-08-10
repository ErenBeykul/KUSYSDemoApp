using KUSYSDemoApp.DataAccess;
using KUSYSDemoApp.Domain.Constants;
using KUSYSDemoApp.Domain.DBModels;
using KUSYSDemoApp.Domain.DTO;
using KUSYSDemoApp.Domain.Enums;
using KUSYSDemoApp.Domain.Extensions;

namespace KUSYSDemoApp.Service
{
    public class StudentService : IStudentService
    {
        readonly IKUSYSDemoAppContext _context;

        public StudentService(IKUSYSDemoAppContext context)
        {
            _context = context;
        }

        public List<Student> GetList()
        {
            return _context.Students.Where(x => !x.IsDeleted).ToList();
        }

        public Student GetStudent(Guid id)
        {
            return _context.Students.FirstOrDefault(x => x.Id == id && !x.IsDeleted)!;
        }

        public Result Save(Student student)
        {
            Result result = new();

            if (student.Id == Guid.Empty)
            {
                #region Ekleme İşlemleri
                student.CreateDate = DateTime.Now;
                _context.Students.Add(student);
                #endregion

            }
            else
            {
                #region Düzenleme İşlemleri
                Student dataStudent = GetStudent(student.Id);

                if (dataStudent == null)
                {
                    result.Type = ResultName.Warning.ToLowerString();
                    result.Message = ResultMessages.NonExistingData;

                    return result;
                }

                dataStudent.Name = student.Name;
                dataStudent.Surname = student.Surname;
                dataStudent.BirthDate = student.BirthDate;
                dataStudent.UpdateDate = DateTime.Now;
                _context.Students.Update(dataStudent);
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

        public Result Delete(List<Guid> ids)
        {
            Result result = new();
            var dataStudents = _context.Students.Where(x => ids.Contains(x.Id) && !x.IsDeleted);
            foreach (var item in dataStudents) item.IsDeleted = true;

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