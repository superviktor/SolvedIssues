using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
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
            //var repo = CreateContribRepo();
            GetAll(repo);
            Insert(repo);
            FindById(repo);
            Update(repo);
            Delete(repo);

            GetFullContact(repo);

            Console.WriteLine("End");
            Console.ReadKey();
        }

        static void GetFullContact(IContactRepo repo)
        {
            var res = repo.GetFullContact(1);
            Console.WriteLine(res.Serialize());
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
            var c = repo.GetFullContact(1);
            c.FirstName = "Edited";
            c.Addresses[0].StreetAddress = "Edited";
            repo.Save(c);
            c = repo.GetFullContact(1);
            Debug.Assert(c.FirstName == "Edited");
            Debug.Assert(c.Addresses[0].StreetAddress == "Edited");
            Console.WriteLine(c.Serialize());
        }

        static void FindById(IContactRepo repo)
        {
            var c = repo.GetFullContact(1);
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
                Title = "Dev",
                Addresses = { new Address
                {
                    AddressType = "Home",
                    City = "Kiev",
                    ContactId = 1,
                    IsDeleted = false,
                    PostalCode = "32057",
                    StateId = 1,
                    StreetAddress = "Khreshchatyk"
                }}
            };

            repo.Save(contact);

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

        private static IContactRepo CreateContribRepo()
        {
            var connectionString = _config.GetConnectionString("Default");
            return new ContactRepoContrib(connectionString);
        }
    }
}
