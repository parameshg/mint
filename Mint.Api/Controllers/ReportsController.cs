using Microsoft.AspNetCore.Mvc;
using Mint.Business.Services.Interfaces;
using Mint.Reports;
using System;
using System.Threading.Tasks;

namespace Mint.Api.Controllers
{
    [Produces("application/json")]
    public class ReportsController : Controller
    {
        public IReportService Server { get; }

        public ReportsController(IReportService server)
        {
            Server = server;
        }

        [HttpGet("reports/GetSummaryReport")]
        [HttpGet("reports/GetSummaryReport/{year:int}")]
        [HttpGet("reports/GetSummaryReport/{year:int}/{month:int}")]
        public async Task<SummaryReport> GetSummaryReport(int year = 0, int month = 0)
        {
            SummaryReport result = null;

            year = year.Equals(0) ? DateTime.Today.Year : year;
            month = month.Equals(0) ? DateTime.Today.Month : month;

            result = await Server.GetSummaryReport(year, month);

            return result;
        }

        [HttpGet("reports/CategoryWiseReport")]
        [HttpGet("reports/CategoryWiseReport/{year:int}")]
        [HttpGet("reports/CategoryWiseReport/{year:int}/{month:int}")]
        public async Task<CategoryWiseReport> GetCategoryWiseReport(int year = 0, int month = 0)
        {
            CategoryWiseReport result = null;

            year = year.Equals(0) ? DateTime.Today.Year : year;
            month = month.Equals(0) ? DateTime.Today.Month : month;

            result = await Server.GetCategoryWiseReport(year, month);

            return result;
        }
    }
}