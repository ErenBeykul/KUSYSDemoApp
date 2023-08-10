using Lamar;
using KUSYSDemoApp.Service.Test.DI;

namespace KUSYSDemoApp.Service.Test
{
    public class StudentCourseServiceTest : IClassFixture<LamarContainerFactory>
    {
        readonly IContainer _container;
        readonly IStudentCourseService _courseService;

        public StudentCourseServiceTest(LamarContainerFactory factory)
        {
            _container = factory.Container;
            _courseService = _container.GetInstance<IStudentCourseService>();
        }

        [Fact]
        public void GetList()
        {
            _courseService.GetList();
        }

        [Fact]
        public void GetListByStudentId()
        {
            _courseService.GetList(new Guid("6521b24c-34d8-4265-8d9d-d6f5a83eb3cf"));
        }

        [Fact]
        public void GetCourses()
        {
            _courseService.GetCourses();
        }

        [Fact]
        public void GetCoursesByStudentId()
        {
            _courseService.GetCourses(new Guid("6521b24c-34d8-4265-8d9d-d6f5a83eb3cf"));
        }

        [Fact]
        public void GetStudentAndCourses()
        {
            _courseService.GetStudentAndCourses(new Guid("6521b24c-34d8-4265-8d9d-d6f5a83eb3cf"));
        }

        [Fact]
        public void Save()
        {
            List<string> courseIds = new() { "MAT101", "PHY101" };
            _courseService.Save(new Guid("6521b24c-34d8-4265-8d9d-d6f5a83eb3cf"), courseIds);
        }

        [Fact]
        public void Delete()
        {
            _courseService.Delete(new List<Guid>());
        }
    }
}