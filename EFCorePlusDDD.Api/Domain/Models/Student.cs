using System.Collections.Generic;
using System.Linq;

namespace EFCorePlusDDD.Api.Domain.Models
{
    public class Student : Entity
    {
        protected Student()
        {
        }

        public Student(string name, string email, Course favoriteCourse, Grade favoriteCourseGrade):this()
        {
            Name = name;
            Email = email;
            FavoriteCourse = favoriteCourse;
            EnrollIn(favoriteCourse, favoriteCourseGrade);
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public virtual Course FavoriteCourse { get; set; }

        private readonly List<Enrollment> _enrollments = new List<Enrollment>();
        public virtual IReadOnlyList<Enrollment> Enrollments => _enrollments.ToList();

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
    }
}
