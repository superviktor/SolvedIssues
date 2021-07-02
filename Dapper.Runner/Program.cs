using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using Dapper.Data.Models;
using Dapper.Data.Repos;
using System.Collections.Generic;
using System.Linq;

namespace Dapper.Runner
{
    class Program
    {
        private static IConfigurationRoot _config;
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            Init();
            //var repo = CreateRepo();
            //var repo = CreateContribRepo();
            //var repo = CreateRepoSP();
            //GetAll(repo);
            //Insert(repo);
            //FindById(repo);
            //Update(repo);
            //Delete(repo);
            //GetFullContact(repo);

            List_support_should_produce_correct_results();
            Dynamic_support_should_produce_correct_results();
            Bulk_insert_should_insert_4_rows();
            GetIllinoisAddresses();
            Get_all_should_return_6_results_with_addresses();

            Console.WriteLine("End");
            Console.ReadKey();
        }

        static void Get_all_should_return_6_results_with_addresses()
        {
            var repository = CreateRepoEx();

            // act
            var contacts = repository.GetAllContactsWithAddresses();

            // assert
            Console.WriteLine($"Count: {contacts.Count}");
            Console.WriteLine(contacts.Serialize());
            Debug.Assert(contacts.First().Addresses.Count == 2);
        }

        static void GetIllinoisAddresses()
        {
            // arrange
            var repository = CreateRepoEx();

            // act
            var addresses = repository.GetAddressesByState(17);

            // assert
            Debug.Assert(addresses.Count == 2);
            Console.WriteLine(addresses.Serialize());
        }

        static void Bulk_insert_should_insert_4_rows()
        {
            // arrange
            var repository = CreateRepoEx();
            var contacts = new List<Contact>
            {
                new Contact { FirstName = "Charles", LastName = "Barkley" },
                new Contact { FirstName = "Scottie", LastName = "Pippen" },
                new Contact { FirstName = "Tim", LastName = "Duncan" },
                new Contact { FirstName = "Patrick", LastName = "Ewing" }
            };

            // act
            var rowsAffected = repository.BulkInsertContacts(contacts);

            // assert
            Console.WriteLine($"Rows inserted: {rowsAffected}");
            Debug.Assert(rowsAffected == 4);
        }

        static void Dynamic_support_should_produce_correct_results()
        {
            // arrange
            var repository = CreateRepoEx();

            // act
            var contacts = repository.GetDynamicContactsById(1, 2, 4);

            // assert
            Debug.Assert(contacts.Count == 3);
            Console.WriteLine($"First FirstName is: {contacts.First().FirstName}");
            Console.WriteLine(contacts.Serialize());
        }

        static void List_support_should_produce_correct_results()
        {
            // arrange
            var repository = CreateRepoEx();

            // act
            var contacts = repository.GetContactsById(1, 2, 4);

            // assert
            Debug.Assert(contacts.Count == 3);
            Console.WriteLine(contacts.Serialize());
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
        private static IContactRepo CreateRepoSP()
        {
            var connectionString = _config.GetConnectionString("Default");
            return new ContactRepoSP(connectionString);
        }
        private static ContactRepositoryEx CreateRepoEx()
        {
            var connectionString = _config.GetConnectionString("Default");
            return new ContactRepositoryEx(connectionString);
        }
    }
}
