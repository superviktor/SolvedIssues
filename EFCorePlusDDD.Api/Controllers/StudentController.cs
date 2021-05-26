using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EFCorePlusDDD.Api.Dtos;
using EFCorePlusDDD.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EFCorePlusDDD.Api.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolContext _schoolContext;
        private readonly StudentRepo _studentRepo;
        public StudentController(SchoolContext schoolContext, StudentRepo studentRepo)
        {
            _schoolContext = schoolContext;
            _studentRepo = studentRepo;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollIn dto)
        {
            //1st way to load one to many collection
            //var student = _schoolContext.Students
            //    .Include(x => x.Enrollments)
            //    .Single(x => x.Id == dto.StudentId);

            //2nd way to load one to many collection
            //var student = _schoolContext.Students.Find(dto.StudentId);
            //_schoolContext.Entry(student).Collection(x=>x.Enrollments).Load();

            var student = _studentRepo.GetById(dto.StudentId);
            var course = _schoolContext.Courses.Find(dto.CourseId);

            //violate encapsulation
            //student.Enrollments.Add(new Enrollment(dto.Grade, course, student));
            var result = student.EnrollIn(dto.Grade, course);
            _schoolContext.SaveChanges();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult DisenrollStudent(Disenroll dto)
        {
            var student = _studentRepo.GetById(dto.StudentId);
            var course = _schoolContext.Courses.Find(dto.CourseId);

            student.Disenroll(course);
            _schoolContext.SaveChanges();

            return Ok();
        }
    }
}
