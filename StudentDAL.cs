
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace QuanLySinhVienCRUD
{
    public class StudentDAL
    {
        public List<Student> GetAll()
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.Students
                    .AsNoTracking()
                    .OrderBy(s => s.Id)
                    .ToList();
            }
        }

        public void Add(Student student)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Students.Add(student);
                db.SaveChanges();
            }
        }

        public void Update(Student student)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Students.Update(student);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Student? student = db.Students.Find(id);

                if (student != null)
                {
                    db.Students.Remove(student);
                    db.SaveChanges();
                }
            }
        }
    }
}
