﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Validation.Domain;

namespace Validation.Api.Controllers
{
    [Route("api/students")]
    public class StudentController : ApplicationController
    {
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;

        public StudentController(StudentRepository studentRepository, CourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var addresses = request.Addresses
                .Select(a => new Address(a.Street, a.City, a.PostalCode))
                .ToArray();

            var email = Email.Create(request.Email);
            var studentName = StudentName.Create(request.Name);
            var student = new Student(email.Value, studentName.Value, addresses);
            _studentRepository.Save(student);

            var response = new RegisterResponse
            {
                Id = student.Id
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult EditPersonalInfo(long id, [FromBody] EditPersonalInfoRequest request)
        {
            Student student = _studentRepository.GetById(id);
            var addresses = request.Addresses
               .Select(a => new Address(a.Street, a.City, a.PostalCode))
               .ToArray();
            //student.EditPersonalInfo(request.Name, addresses);
            _studentRepository.Save(student);

            return Ok();
        }

        [HttpPost("{id}/enrollments")]
        public IActionResult Enroll(long id, [FromBody] EnrollRequest request)
        {
            Student student = _studentRepository.GetById(id);

            foreach (CourseEnrollmentDto enrollmentDto in request.Enrollments)
            {
                Course course = _courseRepository.GetByName(enrollmentDto.Course);
                var grade = Enum.Parse<Grade>(enrollmentDto.Grade);

                student.Enroll(course, grade);
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            Student student = _studentRepository.GetById(id);
            var resonse = new GetResonse
            {
                Addresses = student.Addresses.Select(x => new AddressDto
                {
                    Street = x.Street,
                    City = x.City,
                    PostalCode = x.PostalCode
                }).ToArray(),
                Email = student.Email.Value,
                Name = student.Name.Value,
                Enrollments = student.Enrollments.Select(x => new CourseEnrollmentDto
                {
                    Course = x.Course.Name,
                    Grade = x.Grade.ToString()
                }).ToArray()
            };
            return Ok(resonse);
        }
    }
}
