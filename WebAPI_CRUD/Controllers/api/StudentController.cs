using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI_CRUD.Filters;
using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.Controllers
{
    public class StudentController : ApiController
    {
        //localhost:52971/api/student
        [Log]
        public IHttpActionResult GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (var context = new StudentDbEntities())
            {
                students = context.Students.ToList();
            }

            if (students.Count > 0)
                return Ok(students);
            else
                return Ok("No students found");
        }
        //localhost:52971/api/student/2
        public IHttpActionResult GetStudentById(int id)
        {
            Student student = new Student();
            using (var context = new StudentDbEntities())
            {
                student = context.Students.Where(s => s.Id == id).FirstOrDefault();
            }
            if (student == null)
                return Ok("No student with given ID found");
            else
                return Ok(student);
        }
        //localhost:52971/api/student?name=brinda&email=brinda@gmail.com
        public IHttpActionResult GetStudentsByName(string Name, string Email)
        {
            List<Student> students = new List<Student>();
            using (var context = new StudentDbEntities())
            {
                students = context.Students.Where(s => s.Name == Name & s.Email == Email).ToList();
            }
            if (students.Count > 0)
                return Ok(students);
            else
                return Ok("No students found with the given name and email");
        }
        //localhost:52971/api/student? departmentid = 1
        public IHttpActionResult GetAllStudentsInSameStandard(int departmentId)
        {
            List<Student> students = new List<Student>();
            using (var context = new StudentDbEntities())
            {
                students = context.Students.Where(s => s.departmentId == departmentId).ToList();
            }
            if (students.Count > 0)
                return Ok(students);
            else
                return Ok("No students found in the given standard");
        }

        public IHttpActionResult PostNewStudent(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid student.");

            using (var context = new StudentDbEntities())
            {
                context.Students.Add(new Student()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    departmentId = student.departmentId
                });
                context.SaveChanges();
            }
            return Ok("New Student Added");
        }

        public IHttpActionResult Put(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid student");
            using (var context = new StudentDbEntities())
            {
                var existingStudent = context.Students.Where(s => s.Id == student.Id).FirstOrDefault();
                if (existingStudent == null)
                    return NotFound();
                else
                {
                    existingStudent.Name = student.Name;
                    existingStudent.Email = student.Email;
                    existingStudent.departmentId = student.departmentId;
                    context.SaveChanges();
                }
                return Ok();
            }
        }

        public IHttpActionResult Delete(int id)
        {
            Student student = new Student();
            if (id <= 0)
                return BadRequest("Not a valid student id");
            using (var context = new StudentDbEntities())
            {
                student = context.Students.Where(s => s.Id == id).FirstOrDefault();
                if (student == null)
                    return NotFound();
                else
                {
                    context.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            return Ok();
        }
    }
}