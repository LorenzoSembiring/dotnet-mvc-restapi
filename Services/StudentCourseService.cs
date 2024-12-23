using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repositories;
using System;

namespace WebApplication1.Services
{
    public class StudentCourseService : IScopedService
    {
        private readonly DataContext db;

        public StudentCourseService(DataContext db)
        {
            this.db = db;
        }

        public List<StudentCourses> Current()
        {
            StudentCourses result = new StudentCourses();
            List<StudentCourses> data = this.db.Student_course.ToList();
            return data;
        }

        public CreateStudentCourseDto Create(CreateStudentCourseDto input)
        {
            using (var tran = this.db.Database.BeginTransaction())

                try
                {
                    Console.WriteLine(input.Score);
                    if (input.Score == 0)
                    {
                    StudentCourses sc = new StudentCourses();
                    sc.EducId = input.EducId;
                    sc.StudentId = input.StudentId;
                    //sc.Score = input.Score;
                    this.db.Student_course.Add(sc);
                    this.db.SaveChanges();
                    tran.Commit();
                        //Console.WriteLine(sc);
                    return input;
                    }
                    else
                    {
                        var count = this.db.Student_course
                                           .Where(x => x.StudentId == input.StudentId)
                                           .Count();
                        input.Average = input.Score / count;
                        return input; 
                    }
                }
                catch (Exception ex)
                {
                    {
                        tran.Rollback();
                        return input;
                    }

                }

        }
        public CreateStudentCourseDto CurrentUpdate(CreateStudentCourseDto input)
        {
            using (var tran = this.db.Database.BeginTransaction())

                try
                {
                    var studentCourse = this.db.Student_course.Where(x => x.MappId == input.MappId).FirstOrDefault();

                    if (studentCourse == null)
                    {
                        return null;
                    }

                    studentCourse.EducId = input.EducId;
                    studentCourse.StudentId = input.StudentId;

                    this.db.Entry(studentCourse).State = EntityState.Modified;
                    this.db.SaveChanges();

                    tran.Commit();
                    return input;
                }   
                catch (Exception ex)
                {
                    {
                        tran.Rollback();
                        return input;
                    }

                }
        }
        public List<StudentCourseDto> Join()
        {
            var data = this.db.Student_course   
                .Join(this.db.Students,
                    sc => sc.MappId,
                    s => s.StudentId,
                    (sc, s) => new { sc, s })

                .Join(this.db.Educations,
                        c => c.s.StudentId,
                        e => e.EducId,
                        (c, e) => new StudentCourseDto
                        {
                            StudentFirstName = c.s.FirstName,
                            StudentLastName = c.s.LastName,
                            EducationName = e.EducName
                        }
                    
                        )
                    .ToList();

            return data;
        }

    }
}
