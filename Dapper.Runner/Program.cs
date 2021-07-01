using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Dapper.Data.Models;
using Dapper.Data.Repos;

namespace Dapper.Runner
{
    class Program
    {
        private static IConfigurationRoot _config;
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Init();
            var repo = CreateRepo();
            //GetAll(repo);
            //Insert(repo);
            //FindById(repo);
            //Update(repo);
            Delete(repo);
            Console.WriteLine("End");
            Console.ReadKey();
        }

        static void Delete(IContactRepo repo)
        {
            var contact = new Contact
            {
                FirstName = "Viktor",
                LastName = "Prykhidko",
                Email = "vp@example.com",
                Company = "xyz",
                Title = "Dev"
            };
            repo.Add(contact);
            var c = repo.Find(contact.Id);
            repo.Remove(c.Id);
            c = repo.Find(contact.Id);
            Debug.Assert(c == null);
            Console.WriteLine("Deleted");
        }
        static void Update(IContactRepo repo)
        {
            var c = repo.Find(1);
            c.FirstName = "Edited";
            repo.Update(c);
            c = repo.Find(1);
            Debug.Assert(c.FirstName == "Edited");
            Console.WriteLine(c.Serialize());
        }

        static void FindById(IContactRepo repo)
        {
            var c = repo.Find(1);
            Console.WriteLine(c.Serialize());
        }

        static void Insert(IContactRepo repo)
        {
            var contact = new Contact
            {
                FirstName = "Viktor",
                LastName = "Prykhidko",
                Email = "vp@example.com",
                Company = "xyz",
                Title = "Dev"
            };

            repo.Add(contact);

            Debug.Assert(contact.Id != 0);
            Console.WriteLine(contact.Id);
        }

        static void GetAll(IContactRepo repo)
        {
            var contacts = repo.GetAll();
            Debug.Assert(contacts.Count > 0);
            Console.WriteLine(contacts.Serialize());
        }

        private static void Init()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            _config = builder.Build();
        }

        private static IContactRepo CreateRepo()
        {
            var connectionString = _config.GetConnectionString("Default");
            return new ContactRepo(connectionString);
        }
    }
}
