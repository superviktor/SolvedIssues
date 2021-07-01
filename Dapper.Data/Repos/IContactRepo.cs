using System.Collections.Generic;
using Dapper.Data.Models;

namespace Dapper.Data.Repos
{
    public interface IContactRepo
    {
        Contact Find(int id);
        List<Contact> GetAll();
        Contact Add(Contact contact);
        Contact Update(Contact contact);
        void Remove(int id);
    }
}