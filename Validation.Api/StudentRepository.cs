using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Validation.Domain;

namespace Validation.Api
{
    public class StudentRepository
    {
        private static readonly List<Student> _existingStudents = new List<Student>
        {
            Alice(),
            Bob()
        };
        private static long _lastId = _existingStudents.Max(x => x.Id);

        public Student GetById(long id)
        {
            // Retrieving from the database
            return _existingStudents.SingleOrDefault(x => x.Id == id);
        }

        public void Save(Student student)
        {
            // Setting up the id for new students emulates the ORM behavior
            if (student.Id == 0)
            {
                _lastId++;
                SetId(student, _lastId);
            }

            // Saving to the database
            _existingStudents.RemoveAll(x => x.Id == student.Id);
            _existingStudents.Add(student);
        }

        private static void SetId(Entity entity, long id)
        {
            // The use of reflection to set up the Id emulates the ORM behavior
            entity.GetType().GetProperty(nameof(Entity.Id)).SetValue(entity, id);
        }

        private static Student Alice()
        {
            var alice = new Student(Email.Create("alice@gmail.com").Value, "Alice Alison", new[] { Address.Create("5 Avenue", "New York", "123","DC", new []{"VA","DC"}).Value });
            SetId(alice, 1);
            alice.Enroll(new Course(1, "Calculus", 5), Grade.A);

            return alice;
        }

        private static Student Bob()
        {
            var bob = new Student(Email.Create("bob@gmail.com").Value, "Bob Bobson", new[] { Address.Create("B.Hmyri", "Kiev", "03056","VA", new[]{ "VA", "DC" }).Value });
            SetId(bob, 2);
            bob.Enroll(new Course(2, "History", 4), Grade.B);

            return bob;
        }

        public Student GetByEmail(Email email)
        {
            return _existingStudents.SingleOrDefault(x => x.Email == email);
        }
    }
}
