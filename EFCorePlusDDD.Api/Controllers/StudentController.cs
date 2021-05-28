using System.Linq;
using EFCorePlusDDD.Api.Domain.Models;
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
            var course = Course.FromId(dto.CourseId);

            //violate encapsulation
            //student.Enrollments.Add(new Enrollment(dto.Grade, course, student));
            var result = student.EnrollIn(course, dto.Grade);

            _schoolContext.SaveChanges();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult DisenrollStudent(Disenroll dto)
        {
            var student = _studentRepo.GetById(dto.StudentId);
            var course = Course.FromId(dto.CourseId);

            student.Disenroll(course);

            _schoolContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult RegisterStudent(RegisterStudent dto)
        {
            var course = Course.FromId(dto.FavoriteCourseId);
            var suffix = Suffix.FromId(dto.NameSuffixId);
            //course is not from db context so state is detached if we do
            //_schoolContext.Students.Add(student);
            //here course state is added and 
            //_schoolContext.SaveChanges(); causes
            // error because course is already exists
            //old way to fix
            //_schoolContext.Entry(course).State == EntityState.Unchanged;

            var emailResult = Email.Create(dto.Email);
            if (emailResult.IsFailure)
                return BadRequest(emailResult.Error);

            var nameResult = Name.Create(dto.FirstName, dto.LastName, suffix);
            if (nameResult.IsFailure)
                return BadRequest(nameResult.Error);

            var student = new Student(nameResult.Value, emailResult.Value, course, dto.FavoriteCourseGrade);

            _studentRepo.Save(student);
            _schoolContext.SaveChanges();
            
            return Ok();
        }


        [HttpPost]
        public IActionResult EditPersonalInfo(EditPersonalInfo dto)
        {
            var student = _studentRepo.GetById(dto.StudentId);
            var favoriteCourse = Course.FromId(dto.FavoriteCourseId);
            var suffix = Suffix.FromId(dto.NameSuffixId);

            var emailResult = Email.Create(dto.Email);
            if (emailResult.IsFailure)
                return BadRequest(emailResult.Error);

            var nameResult = Name.Create(dto.FirstName, dto.LastName, suffix);
            if (nameResult.IsFailure)
                return BadRequest(nameResult.Error);

            student.EditPersonalInfo(nameResult.Value, emailResult.Value, favoriteCourse);

            _schoolContext.SaveChanges();

            return Ok();
        }
    }
}
