using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDo
{
    public interface IDashboardService
    {
        Result<bool> EditProject(int projectId, string NewProjectName, string newProjectCategory, decimal newProjectGoal);

        Result<bool> AddDescription(int userId, int projectId, string descriptionText);//saves json or txt on server

        Result<bool> StatusUpdate(int userId, int projectId, string updateText);

        Result<bool> AddRewardPackage(int userId, int projectId, string packageName, string rewardName, decimal Price);

        Result<bool> DeleteRewardPackage(int userId, int projectId, int rewardPackageId);

        Result<bool> DeleteProject(int userId, int projectId);

        Result<string> GetFinancialProgress(int projectId);
    }
}
