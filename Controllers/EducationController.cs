using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/education")]
    public class EducationController : Controller
    {
        private readonly EducationService _educationService;

        public EducationController(EducationService educationService)
        {
            _educationService = educationService;
        }
        [HttpGet("")]

        public ActionResult<List<Educations>> GetAccount()
        {
            List<Educations> user = _educationService.Current();

            return Ok(user);
        }
        [HttpPost("post")]
        public ActionResult<List<EducationDto>> PostAccount(EducationDto input)
        {
            EducationDto user = _educationService.CurrentPost(input);

            return Ok(user);
        }
        [HttpPut("put")]
        public ActionResult<List<EducationDto>> PutAccount(EducationDto input)
        {
            EducationDto user = _educationService.CurrentUpdate(input);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpDelete("delete")]
        public ActionResult<List<EducationDto>> DeleteAccount(EducationDto input)
        {
            EducationDto user = _educationService.CurrentDestroy(input);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
