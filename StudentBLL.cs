
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLySinhVienCRUD.BLL
{
    public class StudentBLL
    {
        private readonly QuanLySinhVienCRUD.StudentDAL dal =
            new QuanLySinhVienCRUD.StudentDAL();

        public List<Student> GetAll()
        {
            return dal.GetAll();
        }

        public List<Student> GetStudentsPassed()
        {
            return dal.GetAll()
                .Where(s => s.Grade >= 5)
                .ToList();
        }

        public void Add(Student student)
        {
            ValidateStudent(student);
            dal.Add(student);
        }

        public void Update(Student student)
        {
            if (student.Id <= 0)
            {
                throw new Exception("Mã sinh viên không hợp lệ!");
            }

            ValidateStudent(student);
            dal.Update(student);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Vui lòng chọn sinh viên!");
            }

            dal.Delete(id);
        }

        private void ValidateStudent(Student student)
        {
            if (student == null)
            {
                throw new Exception("Dữ liệu sinh viên không hợp lệ!");
            }

            if (string.IsNullOrWhiteSpace(student.FullName))
            {
                throw new Exception("Vui lòng nhập họ tên!");
            }

            if (student.FullName.Trim().Length > 100)
            {
                throw new Exception(
                    "Họ tên không được vượt quá 100 ký tự!");
            }

            if (student.Grade < 0 || student.Grade > 10)
            {
                throw new Exception("Điểm phải từ 0 đến 10!");
            }

            student.FullName = student.FullName.Trim();
        }
    }
}
