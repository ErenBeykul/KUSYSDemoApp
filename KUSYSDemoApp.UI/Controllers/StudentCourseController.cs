using KUSYSDemoApp.Domain.Constants;
using KUSYSDemoApp.Domain.DBModels;
using KUSYSDemoApp.Domain.DTO;
using KUSYSDemoApp.Domain.Enums;
using KUSYSDemoApp.Domain.Extensions;
using KUSYSDemoApp.Service;
using KUSYSDemoApp.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KUSYSDemoApp.UI.Controllers;

public class StudentCourseController : BaseController
{
    private readonly IStudentService _studentService;
    private readonly IStudentCourseService _courseService;

    public StudentCourseController(IStudentService studentService, IStudentCourseService courseService)
    {
        _courseService = courseService;
        _studentService = studentService;
    }

    /// <summary>
    /// Öğrenci ve Derslerini Listeler
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        try
        {
            List<StudentCourseViewModel> models = new();
            List<StudentCourseData> studentCourses = new();
            if (IsAdmin) studentCourses = _courseService.GetList();
            else if (CurrentStudentId != null) studentCourses = _courseService.GetList(CurrentStudentId.Value);
            models = Mapper.Map<List<StudentCourseData>, List<StudentCourseViewModel>>(studentCourses);
            return View(models);

        }
        catch (Exception ex)
        {
            return View("~/Views/System/Error.cshtml");
        }
    }

    /// <summary>
    /// Öğrenci Ders Düzenleme Formu
    /// </summary>
    /// <returns></returns>
    public IActionResult Form()
    {
        try
        {
            ViewBag.IsAdmin = IsAdmin;
            StudentCourseData data = new();
            List<SelectListItem> courses = new();
            List<SelectListItem> students = new();
            List<Course> dataCourses = _courseService.GetCourses();            
            foreach (var item in dataCourses) courses.Add(new SelectListItem { Value = item.Id, Text = item.Id + " - " + item.Name });

            if (IsAdmin)
            {
                List<Student> dataStudents = _studentService.GetList();
                foreach (var item in dataStudents) students.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name + " " + item.Surname });
            }
            else if (CurrentStudentId != null)
            {
                data = _courseService.GetStudentAndCourses(CurrentStudentId.Value);
            }                

            StudentCourseViewModel model = new()
            {
                StudentId = CurrentStudentId != null ? CurrentStudentId.Value : Guid.Empty,
                CourseIds=data.CourseIds,
                Student = data.Student,
                Students = students,
                Courses = courses
            };

            return View(model);

        }
        catch (Exception ex)
        {
            return View("~/Views/System/Error.cshtml");
        }
    }

    /// <summary>
    /// Belli Bir Öğrenciye Ait Ders Listesini Elde Eder
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    [HttpPost]
    public JsonResult GetCourses(Guid studentId)
    {
        try
        {
            List<string> courseIds = _courseService.GetCourses(studentId);
            return Json(courseIds);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    /// <summary>
    /// Öğrenci Ders Düzenleme İşlemleri
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public JsonResult Form(StudentCourseViewModel model)
    {
        Result result = new();

        #region Zorunlu Alan Kontrolleri
        if (model.StudentId == Guid.Empty)
        {
            result.Type = ResultName.Warning.ToLowerString();
            result.Message = ResultMessages.InputEmpty;
            return Json(result);
        }
        #endregion

        try
        {
            _courseService.Save(model.StudentId, model.CourseIds);

            result.IsSuccess = true;
            result.Type = ResultName.Success.ToLowerString();
            result.Message = ResultMessages.Success;
        }
        catch (Exception ex)
        {
            result.Type = ResultName.Error.ToLowerString();
            result.Message = ResultMessages.Error;
        }

        return Json(result);
    }

    /// <summary>
    /// Belli Bir Öğrenci ve Ders Listesini Siler
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpPost]
    public JsonResult Delete(List<Guid> ids)
    {
        Result result = new();

        #region Zorunlu Alan Kontrolleri
        if (ids.Count == 0)
        {
            result.Type = ResultName.Warning.ToLowerString();
            result.Message = ResultMessages.RecordSelection;
            return Json(result);
        }
        #endregion

        try
        {
            result = _courseService.Delete(ids);
        }
        catch (Exception ex)
        {
            result.Type = ResultName.Error.ToLowerString();
            result.Message = ResultMessages.Error;
        }

        return Json(result);
    }
}