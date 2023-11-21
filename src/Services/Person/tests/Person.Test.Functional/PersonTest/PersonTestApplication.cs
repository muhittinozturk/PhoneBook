using Application.Dtos;
using Application.Features.Person.Commands.AddPerson;
using Application.Features.Person.Commands.UpdatePerson;
using Domain.Enums;
using Newtonsoft.Json;
using Person.Test.Functional.Base;
using System.Text;
using static Person.Test.Functional.PersonTest.PersonTestEndpoints;

namespace Person.Test.Functional.PersonTest
{
    public class PersonTestApplication
    {
        private const string GET_SINGLE_PERSON_UUID = "1fbaa46e-2271-428b-85a4-7da7562b172c";
        private const string DELETE_PERSON_UUID = "1fbaa46e-2271-428b-85a4-7da7562b172c";
        private const string UPDATE_PERSON_UUID = "e2f4d30b-c4a7-46f1-ba5b-3c45f81ba5a7";

        [Fact]
        public async Task get_all_persons_request_and_response_ok_status_code()
        {
            var endpoints = new PersonTestEndpoints();
            using var server = endpoints.CreateServer();
            var response = await server.CreateClient()
                .GetAsync(Get.Persons());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task get_single_person_request_and_response_ok_status_code()
        {
            var endpoints = new PersonTestEndpoints();
            using var server = endpoints.CreateServer();
            var response = await server.CreateClient()
                .GetAsync(Get.PersonById(Guid.Parse(GET_SINGLE_PERSON_UUID)));

            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task create_person_request_and_response_ok_status_code()
        {
            var model = new AddPersonCommandRequest
            {
                FirstName = "Test",
                LastName = "Test",
                Company = "TEST Corp",
                Contacts = new List<CreatePersonContactDto>
                    {
                        new CreatePersonContactDto {
                            Type = ContactType.Email,
                            Content = "test@example.com"
                        },
                        new CreatePersonContactDto {
                            Type = ContactType.PhoneNumber,
                            Content = "11111111",
                        },
                        new CreatePersonContactDto {
                            Type = ContactType.Location,
                            Content = "Van",
                        }
                    }
            };

            var endpoints = new PersonTestEndpoints();
            using var server = endpoints.CreateServer();

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await server.CreateClient()
                .PostAsync(Post.CreatePerson(), content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task update_person_request_and_response_ok_status_code()
        {
            var model = new UpdatePersonCommandRequest
            {
                PersonId = new Guid(UPDATE_PERSON_UUID),
                FirstName = "Test Update",
                LastName = "Test Update",
                Company = "TEST Corp Update",
            };

            var endpoints = new PersonTestEndpoints();
            using var server = endpoints.CreateServer();

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await server.CreateClient()
                .PutAsync(Put.UpdatePerson(), content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task delete_person_request_and_response_ok_status_code()
        {
            var endpoints = new PersonTestEndpoints();
            using var server = endpoints.CreateServer();
            var response = await server.CreateClient()
                .DeleteAsync(Delete.DeletePerson(Guid.Parse(DELETE_PERSON_UUID)));

            response.EnsureSuccessStatusCode();
        }
    }

}
