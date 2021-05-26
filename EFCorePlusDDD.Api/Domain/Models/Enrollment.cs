namespace EFCorePlusDDD.Api.Domain.Models
{
    public class Enrollment : Entity
    {
        public Grade Grade { get; }
        public virtual Course Course { get; }
        public virtual Student Student { get; }

        protected Enrollment(){}

        public Enrollment(Grade grade, Course course, Student student):this()
        {
            Grade = grade;
            Course = course;
            Student = student;
        }
    }
}