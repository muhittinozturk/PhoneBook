using Microsoft.AspNetCore.TestHost;
using Person.Test.Functional.Base;


namespace Person.Test.Functional.PersonTest
{
    public class PersonTestEndpoints
    {
        public static class Get
        {
            public static string Persons()
            {
                return "api/person/getall";
            }

            public static string PersonById(Guid id)
            {
                return $"api/person/{id}";
            }
        }

        public static class Post
        {
            public static string CreatePerson()
            {
                return $"api/person";
            }
        }

        public static class Put
        {
            public static string UpdatePerson()
            {
                return $"api/person";
            }
        }

        public static class Delete
        {
            public static string DeletePerson(Guid id)
            {
                return $"api/person/{id}";
            }
        }

        public TestServer CreateServer()
        {
            var factory = new BaseHost();
            return factory.Server;
        }
    }
}
