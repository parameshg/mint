using Mint.Reports;
using System.Threading.Tasks;

namespace Mint.Business.Services.Interfaces
{
    public interface IReportService : IService
    {
        Task<CategoryWiseReport> GetCategoryWiseReport(int year, int month = 0);

        Task<SummaryReport> GetSummaryReport(int year, int month = 0);
    }
}