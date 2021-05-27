using EFCorePlusDDD.Api.Domain.Models;

namespace EFCorePlusDDD.Api.Repositories
{
    /// <summary>
    /// The only purpose of this class is to move complexity of loading enrollments for student away from controller 
    /// </summary>
    public class StudentRepo
    {
        private readonly SchoolContext _schoolContext;

        public StudentRepo(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public Student GetById(long id)
        {
            var student = _schoolContext.Students.Find(id);
            if (student == null)
                return null;
            _schoolContext.Entry(student).Collection(x=>x.Enrollments).Load();
            return student;
        }

        public void Save(Student student)
        {
            _schoolContext.Students.Attach(student);
        }
    }
}
