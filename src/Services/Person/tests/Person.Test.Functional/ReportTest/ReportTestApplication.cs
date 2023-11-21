using Application.Dtos;
using Application.Features.Person.Commands.AddPerson;
using Application.Features.Person.Commands.UpdatePerson;
using Domain.Enums;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static Person.Test.Functional.ReportTest.ReportTestEndpoints;

namespace Person.Test.Functional.ReportTest
{
    public class ReportTestApplication
    {
        private const string GET_ALL_REPORT_BY_REQUEST_ID = "655caeab5f6aa45dde55cd65";
        private const string GET_ALL_REPORT_BY_REQUEST_FAIL_ID = "";
        
        private const string GET_REPORT_DETAIL_BY_REPORT_DETAL_ID = "c14551bf-60d9-4d56-92ac-a98027cdd093";
        private const string GET_REPORT_DETAIL_BY_REPORT_DETAL_ID_FAIL = "c14341bf-60d9-4d56-92ac-a98027cdd093";

        [Fact]
        public async Task get_all_persons_request_and_response_ok_status_code()
        {
            var endpoints = new ReportTestEndpoints();
            using var server = endpoints.CreateServer();
            var response = await server.CreateClient()
                .GetAsync(Get.Reports(GET_ALL_REPORT_BY_REQUEST_ID));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task get_single_person_request_and_response_ok_status_code()
        {
            var endpoints = new ReportTestEndpoints();
            using var server = endpoints.CreateServer();
            var response = await server.CreateClient()
                .GetAsync(Get.ReportByReportDetailId(Guid.Parse(GET_REPORT_DETAIL_BY_REPORT_DETAL_ID)));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task get_all_persons_request_and_response_not_found_status_code()
        {
            var endpoints = new ReportTestEndpoints();
            using var server = endpoints.CreateServer();
            var response = await server.CreateClient()
                .GetAsync(Get.Reports(GET_ALL_REPORT_BY_REQUEST_FAIL_ID));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task get_single_person_request_and_response_bad_request_status_code()
        {
            var endpoints = new ReportTestEndpoints();
            using var server = endpoints.CreateServer();
            var response = await server.CreateClient()
                .GetAsync(Get.ReportByReportDetailId(Guid.Parse(GET_REPORT_DETAIL_BY_REPORT_DETAL_ID_FAIL)));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
