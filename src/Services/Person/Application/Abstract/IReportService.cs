using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IReportService : IBaseService<Report>
    {
        Task<CreateReportDto> GenerateReportAsync(string reportId);
        Task<GetReportDetail> GetReportDetailById(Guid reportDetailId);
        Task<List<GetReportDetail>> GetAllReportDetail(string reportId);
    }
}
