using Microsoft.AspNetCore.Mvc.Rendering;

namespace KUSYSDemoApp.UI.ViewModels;

public class StudentCourseViewModel
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string? Student { get; set; }
    public string? CourseCode { get; set; }
    public string? CourseName { get; set; }
    public string? CreateDate { get; set; }
    public List<string> CourseIds { get; set; } = new();
    public List<SelectListItem> Courses { get; set; } = new();
    public List<SelectListItem> Students { get; set; } = new();
}