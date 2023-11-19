using Application.Abstract;
using Application.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ReportManager : BaseManager<Report>, IReportService
    {
        public ReportManager(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CreateReportDto> GenerateReportAsync(string id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var locations = _context.Contacts
                        .Where(ci => ci.Type == ContactType.Location)
                        .Select(ci => ci.Content)
                        .Distinct()
                        .ToList();

                    Report report = new Report
                    {
                        Id = id,
                    };

                    List<ReportDetail> reportDetails = new List<ReportDetail>();

                    foreach (var location in locations)
                    {
                        var reportDetail = new ReportDetail
                        {
                            ReportId = id,
                            Location = location,
                            PersonCount = _context.Persons.Count(p => p.Contacts.Any(c => c.Type == ContactType.Location && c.Content == location)),
                            PhoneNumberCount = _context.Contacts.Count(ci => ci.Type == ContactType.PhoneNumber && ci.Person.Contacts.Any(c => c.Type == ContactType.Location && c.Content == location))
                        };

                        reportDetails.Add(reportDetail);
                    }

                    await _context.Reports.AddAsync(report);
                    await _context.ReportDetails.AddRangeAsync(reportDetails);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new()
                    {
                        IsSuccess = true,
                        ReportId = id,
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new()
                    {
                        IsSuccess = false,
                        Message = ex.Message,
                    };
                }
            }
        }

        public async Task<List<GetReportDetail>> GetAllReportDetail(string reportId)
        {
            return await _context.ReportDetails
                .Where(r => r.ReportId == reportId)
                .Select(r => new GetReportDetail()
                {
                    Location = r.Location,
                    PersonCount = r.PersonCount,
                    PhoneNumberCount = r.PhoneNumberCount,
                })
                .ToListAsync();

        }

        public async Task<GetReportDetail> GetReportDetailById(Guid reportDetailId)
        {
            var reportDetail = await _context.ReportDetails.FindAsync(reportDetailId);

            return new()
            {
                Location = reportDetail.Location,
                PersonCount = reportDetail.PersonCount,
                PhoneNumberCount = reportDetail.PhoneNumberCount,
            };
        }
    }
}
