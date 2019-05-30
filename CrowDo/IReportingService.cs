using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDo
{
    public interface IReportingService
    {
        Result<List<Project>> GetRecentProjects();

        Result<List<Project>> GetPopularProjects();

        Result<List<User>> GetTopCreators();

        Result<bool> WeeklyReport(string filename);

        Result<bool> MonthlyReport(string filename);
    }
}
