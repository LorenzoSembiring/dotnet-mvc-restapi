using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class EducationService: IScopedService
    {
        private readonly DataContext db;

        public EducationService(DataContext db)
        {
            this.db = db;
        }

        public List<Educations> Current()
        {
            Educations result = new Educations();
            List<Educations> data = this.db.Educations.ToList();
            return data;
        }
        public EducationDto CurrentPost(EducationDto input)
        {
            using (var tran = this.db.Database.BeginTransaction())

                try
                {
                    Educations e = new Educations();
                    e.EducName= input.EducName;
                    this.db.Educations.Add(e);
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
        public EducationDto CurrentUpdate(EducationDto input)
        {
            using (var tran = this.db.Database.BeginTransaction())

                try
                {
                    var education = this.db.Educations.Where(x => x.EducId == input.EducId).FirstOrDefault();

                    if (education == null)
                    {
                        return null;
                    }

                    education.EducName = input.EducName;

                    this.db.Entry(education).State = EntityState.Modified;
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
        public EducationDto CurrentDestroy(EducationDto input)
        {
            using (var tran = this.db.Database.BeginTransaction())

                try
                {
                    var education = this.db.Educations.Where(x => x.EducId == input.EducId).FirstOrDefault();
                    this.db.Educations.Remove(education);
                    if (education == null)
                    {
                        return null;
                    }
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
