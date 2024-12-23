using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/student")]
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;
        
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet("")]

        public ActionResult<List<Students>> GetAccount()
        {
            List<Students> user = _studentService.Current();

            return Ok(user);
        }
        [HttpPost("post")]
        public ActionResult<List<StudentDto>> PostAccount(StudentDto input)
        {
            StudentDto user = _studentService.CurrentPost(input);

            return Ok(user);
        }
        [HttpPut("put")]
        public ActionResult<List<StudentDto>> PutAccount(StudentDto input)
        {
            StudentDto user = _studentService.CurrentUpdate(input);

            return Ok(user);
        }
    }
}
