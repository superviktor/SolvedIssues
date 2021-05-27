using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCorePlusDDD.Api.Domain.Models
{
    public class Student : Entity
    {
        private Student()
        {
        }

        public Student(Name name, Email email, Course favoriteCourse, Grade favoriteCourseGrade):this()
        {
            Name = name;
            Email = email;
            FavoriteCourse = favoriteCourse;
            EnrollIn(favoriteCourse, favoriteCourseGrade);
        }

        public virtual Name Name { get; private set; }
        public Email Email { get; private set; }
        public virtual Course FavoriteCourse { get; private set; }

        private readonly List<Enrollment> _enrollments = new List<Enrollment>();
        public IReadOnlyList<Enrollment> Enrollments => _enrollments.ToList();

        public string EnrollIn(Course course, Grade grade)
        {
            if (_enrollments.Any(e => e.Course == course))
                return "Already enrolled";
            _enrollments.Add(new Enrollment(grade, course, this));
            return "Successfully enrolled";
        }

        public void Disenroll(Course course)
        {
            var enrollment = _enrollments.FirstOrDefault(x => x.Course == course);
            _enrollments.Remove(enrollment);
        }

        public void EditPersonalInfo(Name name, Email email, Course favoriteCourse)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (email == null)
                throw new ArgumentNullException(nameof(email));
            if (favoriteCourse == null)
                throw new ArgumentNullException(nameof(favoriteCourse));

            Name = name;
            Email = email;
            FavoriteCourse = favoriteCourse;
        }
    }
}
