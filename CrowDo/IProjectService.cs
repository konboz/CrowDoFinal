using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDo
{
    public interface IProjectService
    {
        Result<bool> DeadlineCheck();

        Result<Project> PublishProject(string creatorEmail, string projectName, string category
            , string description, decimal projectGoal, DateTime creationDate, DateTime monthDuration
            , int estimatedMonthDuration);

        //Result<Project> SearchProject(string text);

        Result<List<Project>> SearchByText(string text);

        Result<List<Project>> SearchByCategory(string category);

        Result<List<Project>> SearchByYear(int year);

        Result<List<Project>> SearchByCreator(string name);

        Result<List<Project>> GetPendingProjects();

        Result<List<Project>> GetFundedProjects();

        Result<Project> GetProjectDetails(int projectId); 

        Result<bool> FundProject(int userId, int projectId, int rewardPackageId);
    }
}
