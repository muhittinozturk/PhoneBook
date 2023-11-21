using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Report.Test.Functional.ApplicationTest
{
    public class ReportApplicationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ReportApplicationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task PostReport_Returns_Ok_Result_And_Returns_Value_Is_True()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync("/api/Report", null);

            // Assert
            response.EnsureSuccessStatusCode();

            var expectedMessage = "Rapor oluşturma talebi iletildi.";
            var actualContent = await response.Content.ReadAsStringAsync();

            var actualMessage = JsonSerializer.Deserialize<string>(actualContent);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedMessage, actualMessage, StringComparer.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task GetReports_ReturnsOkResult()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Report");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var reports = JsonSerializer.Deserialize<ReportAPI.Entities.Report[]>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(reports);
            Assert.IsType<ReportAPI.Entities.Report[]>(reports);
        }
    }
}
