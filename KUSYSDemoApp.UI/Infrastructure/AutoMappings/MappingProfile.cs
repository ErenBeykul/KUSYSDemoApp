using AutoMapper;
using KUSYSDemoApp.Domain.DBModels;
using KUSYSDemoApp.Domain.DTO;
using KUSYSDemoApp.UI.ViewModels;

namespace KUSYSDemoApp.UI.AutoMappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentViewModel, Student>().ReverseMap();
            CreateMap<StudentCourseData, StudentCourseViewModel>();
            CreateMap<DateTime, string>().ConvertUsing(dt => dt.ToString("dd.MM.yyyy"));
        }
    }
}