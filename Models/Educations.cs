using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Educations
    {
        [Key]
        public int EducId { get; set; }
        public string EducName { get; set; }
    }

    public class EducationDto
    {
        public int EducId { get; set; }
        public string EducName { get; set; }
    }
}
