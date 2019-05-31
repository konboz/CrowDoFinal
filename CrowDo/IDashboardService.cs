using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDo
{
    public interface IDashboardService
    {

        Result<bool> EditProject(int projectId, string NewProjectName, string newProjectCategory, string description, decimal newProjectGoal,
            DateTime monthDuration, int estimatedMonthDuration);

        Result<bool> StatusUpdate(int userId, int projectId, string updateText);

        Result<bool> AddRewardPackage(int userId, int projectId, string packageName, string rewardName, decimal Price);

        Result<bool> DeleteRewardPackage(int userId, int projectId, int rewardPackageId);

        Result<bool> DeleteProject(int userId, int projectId);

        Result<string> GetFinancialProgress(int projectId);

        Result<bool> AddMultimediaFile(int projectId, string multiFile);

    }
}
