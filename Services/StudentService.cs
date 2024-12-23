using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repositories;
using System;
using System.Text.RegularExpressions;

namespace WebApplication1.Services
{
    public class StudentService : IScopedService
    {
        private readonly DataContext db;

        public StudentService(DataContext db)
        {
            this.db = db;
        }

        public List<Students> Current()
        {
            Students result = new Students();
            List<Students> data = this.db.Students.ToList();
            return data;
        }

        public StudentDto CurrentPost(StudentDto input)
        {
            using (var tran = this.db.Database.BeginTransaction())
            {
                try
                {
                    string patternNumbers = @"\d+";
                    string patternLetters = @"[^\d]+";

                    Regex regexNumbers = new Regex(patternNumbers);
                    Regex regexLetters = new Regex(patternLetters);

                    MatchCollection numberMatches = regexNumbers.Matches(input.LastName);

                    string concatenatedNumbers = string.Concat(numberMatches.Cast<Match>().Select(m => m.Value));
                    int numberName = 0;
                    if (!string.IsNullOrEmpty(concatenatedNumbers))
                    {
                        numberName = int.Parse(concatenatedNumbers);
                    }

                    string alphabeticLastName = string.Concat(regexLetters.Matches(input.LastName).Cast<Match>().Select(m => m.Value));

                    Students s = new Students
                    {
                        FirstName = input.FirstName,
                        LastName = alphabeticLastName,
                        DateOfBirth = input.DateOfBirth,
                        Number = numberName
                    };

                    this.db.Students.Add(s);
                    this.db.SaveChanges();
                    tran.Commit();

                    return input;
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    return input;
                }
            }
        }



        public StudentDto CurrentUpdate(StudentDto input)
        {
            using (var tran = this.db.Database.BeginTransaction())

                try
                {
                    var student = this.db.Students.Where(x => x.StudentId == input.StudentId).FirstOrDefault();

                    if (student == null)
                    {
                        return null;
                    }

                    student.FirstName = input.FirstName;
                    student.LastName = input.LastName;
                    student.DateOfBirth = input.DateOfBirth;

                    this.db.Entry(student).State = EntityState.Modified;
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
    }
}
