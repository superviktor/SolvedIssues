using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper.Contrib.Extensions;
using Dapper.Data.Models;
using Microsoft.Data.SqlClient;

namespace Dapper.Data.Repos
{
    public class ContactRepoContrib : IContactRepo
    {
        private readonly IDbConnection _db;

        public ContactRepoContrib(string cs)
        {
            _db = new SqlConnection(cs);
        }
        public Contact Find(int id)
        {
            return _db.Get<Contact>(id);
        }

        public List<Contact> GetAll()
        {
            return _db.GetAll<Contact>().ToList();
        }

        public Contact Add(Contact contact)
        {
            var id = _db.Insert(contact);
            contact.Id = (int)id;
            return contact;
        }

        public Contact Update(Contact contact)
        {
            _db.Update(contact);
            return contact;
        }

        public void Remove(int id)
        {
            _db.Delete(new Contact { Id = id });
        }

        public Contact GetFullContact(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save(Contact contact)
        {
            throw new System.NotImplementedException();
        }
    }
}