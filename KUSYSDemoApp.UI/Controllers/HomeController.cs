using Microsoft.AspNetCore.Mvc;
using KUSYSDemoApp.Domain.DBModels;
using KUSYSDemoApp.Service;
using KUSYSDemoApp.UI.ViewModels;
using KUSYSDemoApp.Domain.Constants;
using KUSYSDemoApp.Domain.DTO;
using KUSYSDemoApp.Domain.Enums;
using KUSYSDemoApp.Domain.Extensions;

namespace KUSYSDemoApp.UI.Controllers;

public class HomeController : BaseController
{
    private readonly IStudentService _studentService;

    public HomeController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>
    /// Öğrencileri Listeler
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        try
        {
            ViewBag.IsAdmin = IsAdmin;
            List<Student> students = _studentService.GetList();
            List<StudentViewModel> models = Mapper.Map<List<Student>, List<StudentViewModel>>(students);
            return View(models);

        }
        catch (Exception ex)
        {
            return View("~/Views/System/Error.cshtml");
        }        
    }

    /// <summary>
    /// Belli Bir Öğrenciye Ait Detay Bilgilerini Elde Eder
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    public JsonResult GetDetails(Guid id)
    {
        try
        {
            Student student = _studentService.GetStudent(id);
            StudentViewModel model = Mapper.Map<Student, StudentViewModel>(student);
            return Json(model);
        }
        catch (Exception ex)
        {
            throw;
        }        
    }

    // <summary>
    /// Öğrenci Ekleme Düzenleme Formu
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IActionResult Form(Guid id)
    {
        #region Kullanıcının Erişim Yetkisi Var mı Kontrol Ediliyor
        if (!IsAdmin) return View("~/Views/System/Unauthorized.cshtml");
        #endregion

        StudentViewModel model = new();

        try
        {
            if (id != Guid.Empty)
            {
                Student student = _studentService.GetStudent(id);
                model = Mapper.Map<Student, StudentViewModel>(student);
                model.BirthDate = student.BirthDate.ToString("yyyy-MM-dd");
                return View(model);
            }

            return View(model);

        }
        catch (Exception ex)
        {
            return View("~/Views/System/Error.cshtml");
        }
    }

    /// <summary>
    /// Öğrenci Ekleme Düzenleme İşlemleri
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public JsonResult Form(StudentViewModel model)
    {
        Result result = new();

        #region Kullanıcının Erişim Yetkisi Var mı Kontrol Ediliyor
        if (!IsAdmin)
        {
            result.Type = ResultName.Warning.ToLowerString();
            result.Message = ResultMessages.UnauthorizedAction;

            return Json(result);
        }
        #endregion

        #region Zorunlu Alan Kontrolleri
        if (string.IsNullOrEmpty(model.Name)|| string.IsNullOrEmpty(model.Surname)|| string.IsNullOrEmpty(model.BirthDate))
        {
            result.Type = ResultName.Warning.ToLowerString();
            result.Message = ResultMessages.InputEmpty;

            return Json(result);
        }
        #endregion

        try
        {
            Student student = Mapper.Map<StudentViewModel, Student>(model);
            result = _studentService.Save(student);

        }
        catch (Exception ex)
        {
            result.Type = ResultName.Error.ToLowerString();
            result.Message = ResultMessages.Error;
        }

        return Json(result);
    }

    /// <summary>
    /// Belli Bir Öğrenci Listesini Siler
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpPost]
    public JsonResult Delete(List<Guid> ids)
    {
        Result result = new();

        #region Kullanıcının Erişim Yetkisi Var mı Kontrol Ediliyor
        if (!IsAdmin)
        {
            result.Type = ResultName.Warning.ToLowerString();
            result.Message = ResultMessages.UnauthorizedAction;

            return Json(result);
        }
        #endregion

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
            result = _studentService.Delete(ids);
        }
        catch (Exception ex)
        {
            result.Type = ResultName.Error.ToLowerString();
            result.Message = ResultMessages.Error;
        }

        return Json(result);
    }
}