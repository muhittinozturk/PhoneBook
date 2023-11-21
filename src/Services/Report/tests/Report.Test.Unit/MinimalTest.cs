using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using ReportAPI.Data;
using ReportAPI.Enums;
using ReportAPI.Repositories;
using System.Linq.Expressions;

namespace Report.Test.Unit
{
    public class MinimalTest
    {
        [Fact]
        public async Task AddAsync_Should_Add_ReportAsync()
        {
            // Arrange
            var reportRepositoryMock = new Mock<IReportRepository<ReportAPI.Entities.Report>>();

            var report = new ReportAPI.Entities.Report
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Status = ReportStatus.InProgress,
                ResultMessage = "Test Message"
            };

            // Configure the Add method of the repository mock
            reportRepositoryMock.Setup(repo => repo.Add(It.IsAny<ReportAPI.Entities.Report>()));

            // Configure the GetByIdAsync method of the repository mock
            reportRepositoryMock?
                .Setup(repo => repo.GetByIdAsync(It.IsAny<Expression<Func<ReportAPI.Entities.Report, bool>>>()))
                .ReturnsAsync((Expression<Func<ReportAPI.Entities.Report, bool>> predicate) =>
                {
                    // Simulate a scenario where the predicate is used to filter the reports
                    var reports = new List<ReportAPI.Entities.Report>
                    {
                        new ReportAPI.Entities.Report
                        {
                            Id = "1",
                            Status = ReportStatus.InProgress,
                            ResultMessage = "Test Message"
                        },
                        new ReportAPI.Entities.Report
                        {
                            Id = report.Id,
                            Status = ReportStatus.InProgress,
                            ResultMessage = "Test Message"
                        }
                    };

                    return reports.FirstOrDefault(predicate.Compile());
                });

            // Act
            reportRepositoryMock.Object.Add(report);

            var retrievedReport = await reportRepositoryMock.Object.GetByIdAsync(p => p.Id == report.Id);

            // Assert
            // Verify that the Add method was called on the repository mock
            reportRepositoryMock.Verify(repo => repo.Add(It.IsAny<ReportAPI.Entities.Report>()), Times.Once);

            // Additional Assertions
            Assert.NotNull(report);
            Assert.Equal(report.Id, retrievedReport.Id);
            Assert.Equal(report.Status, retrievedReport.Status);
            Assert.Equal(report.ResultMessage, retrievedReport.ResultMessage);

        }

    }
}