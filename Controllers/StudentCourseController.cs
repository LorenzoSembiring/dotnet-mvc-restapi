using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/student-course")]
    public class StudentCourseController : Controller
    {
        private readonly StudentCourseService _studentCourseService;

        public StudentCourseController(StudentCourseService studentCourseService)
        {
            _studentCourseService = studentCourseService;
        }
        [HttpGet("")]

        public ActionResult<List<StudentCourses>> GetAccount()
        {
            List<StudentCourses> data = _studentCourseService.Current();

            return Ok(data);
        }

        [HttpGet("index")]
        public ActionResult<List<StudentCourseDto>> Join()
        {
            List<StudentCourseDto> data = _studentCourseService.Join();

            return Ok(data);
        }
        [HttpPost("post")]
        public ActionResult<List<StudentCourseDto>> PostAccount(CreateStudentCourseDto input)
        {
            CreateStudentCourseDto data = _studentCourseService.Create(input);

            return Ok(data);
        }
        [HttpPut("put")]
        public ActionResult<List<CreateStudentCourseDto>> PutAccount(CreateStudentCourseDto input)
        {
            CreateStudentCourseDto user = _studentCourseService.CurrentUpdate(input);

            return Ok(user);
        }

    }
}
