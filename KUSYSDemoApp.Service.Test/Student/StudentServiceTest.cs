using Lamar;
using KUSYSDemoApp.Domain.DBModels;
using KUSYSDemoApp.Service.Test.DI;

namespace KUSYSDemoApp.Service.Test
{
    public class StudentServiceTest : IClassFixture<LamarContainerFactory>
    {
        readonly IContainer _container;
        readonly IStudentService _studentService;

        public StudentServiceTest(LamarContainerFactory factory)
        {
            _container = factory.Container;
            _studentService = _container.GetInstance<IStudentService>();
        }

        [Fact]
        public void GetList()
        {
            _studentService.GetList();
        }

        [Fact]
        public void GetStudent()
        {
            _studentService.GetStudent(new Guid("6521b24c-34d8-4265-8d9d-d6f5a83eb3cf"));
        }

        [Fact]
        public void Save()
        {
            _studentService.Save(new Student());
        }

        [Fact]
        public void Delete()
        {
            _studentService.Delete(new List<Guid>());
        }
    }
}