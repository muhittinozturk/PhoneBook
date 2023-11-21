using Microsoft.AspNetCore.TestHost;
using Person.Test.Functional.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Test.Functional.ReportTest
{
    public class ReportTestEndpoints
    {
        public static class Get
        {
            public static string Reports(string id)
            {
                return $"api/report/getall/{id}";
            }

            public static string ReportByReportDetailId(Guid id)
            {
                return $"api/report/{id}";
            }
        }

        public TestServer CreateServer()
        {
            var factory = new BaseHost();
            return factory.Server;
        }
    }
}
