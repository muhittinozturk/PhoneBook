using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public class AppContextSeeding
    {
        public void SeedAsync(ApplicationDbContext context)
        {


            if (!context.Persons.Any())
            {
                var persons = GetPersonList();

                foreach (var person in persons)
                {
                    context.Persons.Add(person);

                    var personId = person.UUID;

                    foreach (var contact in person.Contacts)
                    {
                        contact.UUID = Guid.NewGuid();
                        contact.PersonId = personId;
                        context.Contacts.Add(contact);
                    }

                }
                context.SaveChanges();

            }
        }

        private List<Person> GetPersonList()
        {
            var persons = new List<Person>()
            {
                new Person
                {
                    UUID = Guid.Parse("1fbaa46e-2271-428b-85a4-7da7562b172c"),
                    FirstName = "John",
                    LastName = "Doe",
                    Company = "ABC Corp",
                    CreateTime = DateTime.UtcNow,
                    UpdateTime = DateTime.UtcNow,
                    Contacts = new List<Contact>
                    {
                        new Contact {
                            Type = ContactType.Email,
                            Content = "john@example.com",
                            CreateTime = DateTime.UtcNow,
                            UpdateTime = DateTime.UtcNow
                        },
                        new Contact {
                            Type = ContactType.PhoneNumber,
                            Content = "687254629",
                            CreateTime = DateTime.UtcNow,
                            UpdateTime = DateTime.UtcNow
                        },
                        new Contact {
                            Type = ContactType.Location,
                            Content = "Van",
                            CreateTime = DateTime.UtcNow,
                            UpdateTime = DateTime.UtcNow
                        }
                    }
                },
                new Person
                {
                    UUID = Guid.Parse("e2f4d30b-c4a7-46f1-ba5b-3c45f81ba5a7"),
                    FirstName = "Jane",
                    LastName = "Smith",
                    Company = "XYZ Inc",
                    CreateTime = DateTime.UtcNow,
                    UpdateTime = DateTime.UtcNow,
                    Contacts = new List<Contact>
                    {
                        new Contact {
                            Type = ContactType.Email,
                            Content = "jane@example.com",
                            CreateTime = DateTime.UtcNow,
                            UpdateTime = DateTime.UtcNow
                        },
                        new Contact {
                            Type = ContactType.PhoneNumber,
                            Content = "987654321",
                            CreateTime = DateTime.UtcNow,
                            UpdateTime = DateTime.UtcNow
                        },
                        new Contact {
                            Type = ContactType.Location,
                            Content = "Ankara",
                            CreateTime = DateTime.UtcNow,
                            UpdateTime = DateTime.UtcNow
                        }
                    }
                }
            };

            return persons;

        }
    }
}
