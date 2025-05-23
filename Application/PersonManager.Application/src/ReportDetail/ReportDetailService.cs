﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonManager.Application.Abstractions.ReportDetail;
using PersonManager.Application.Abstractions.ReportDetail.Contracts;
using PersonManager.Common.Enums;
using PersonManager.Domain;
using PersonManager.Persistence.Context;

namespace PersonManager.Application.ReportDetail
{
    public class ReportDetailService : IReportDetailService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ReportDetailService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(Guid reportId)
        {

                    var reports = await _context.PersonInfos
            .Where(pi => pi.ContactType == ContactType.Location)
            .GroupBy(pi => pi.Content)
            .Select(group => new ReportDetailDto
            {
                Location = group.Key,
                PersonCount = group.Select(g => g.PersonId).Distinct().Count(),

                PhoneNumberCount = _context.PersonInfos
                    .Where(p2 => p2.ContactType == ContactType.Phone)
                    .Count(p2 =>
                        _context.PersonInfos.Any(p3 =>
                            p3.PersonId == p2.PersonId &&
                            p3.ContactType == ContactType.Location &&
                            p3.Content == group.Key
                        )
                    ),
                CreatedDate = DateTime.UtcNow,
                ReportId = reportId
            })
            .ToListAsync();

            var report = await _context.Reports.Where(p => p.Id == reportId).FirstOrDefaultAsync().ConfigureAwait(false);
            if (report is not null)
            {
                report.ReportStatus = ReportStatus.Completed;
                var reportList = _mapper.Map<List<Domain.ReportDetail>>(reports);

                _context.ReportDetails.AddRange(reportList);
                _context.Reports.Update(report);

                await _context.SaveChangesAsync();
            }

        }

        public async Task<List<ReportDetailDto>> GetByReportDetailListAsync(Guid reportId)
        {
            var details = await _context.ReportDetails.Where(p => p.ReportId == reportId).ToListAsync().ConfigureAwait(false);
            var detailList = _mapper.Map<List<ReportDetailDto>>(details);
            return detailList;
        }
    }
}
