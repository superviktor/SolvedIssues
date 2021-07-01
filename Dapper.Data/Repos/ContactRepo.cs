using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper.Data.Models;
using Microsoft.Data.SqlClient;

namespace Dapper.Data.Repos
{
    public class ContactRepo : IContactRepo
    {
        private readonly IDbConnection _db;

        public ContactRepo(string cs)
        {
            _db = new SqlConnection(cs);
        }
        public Contact Find(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Contact> GetAll()
        {
            return _db.Query<Contact>("SELECT * FROM Contacts").ToList();
        }

        public Contact Add(Contact contact)
        {
            var sql =
                "INSERT INTO Contacts (FirstName, LastName, Email, Company, Title) VALUES(@FirstName, @LastName, @Email, @Company, @Title); " +
                "SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = _db.Query<int>(sql, contact).Single();
            contact.Id = id;
            return contact;
        }

        public Contact Update(Contact contact)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}