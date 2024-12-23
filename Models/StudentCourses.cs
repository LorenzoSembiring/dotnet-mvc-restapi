using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class StudentCourses
    {
        [Key]
        public int MappId { get; set; }
        public int StudentId { get; set; }
        public int EducId { get; set; }
    }

    public class StudentCourseDto
    {
        public int MappId { get; set; }
        public String StudentFirstName { get; set; }
        public String StudentLastName { get; set; }
        public String EducationName { get; set; }
        public int StudentId { get; set; }
        public int EducId { get; set; }
    }

    public class CreateStudentCourseDto
    {
        public int MappId { get; set; }
        public int StudentId { get; set; }
        public int EducId { get; set; }
        public int Score { get; set; }
        public int Average { get; set; }
    }

}
