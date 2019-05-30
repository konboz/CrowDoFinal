using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrowDo
{
    public class DashboardService : IDashboardService
    {


        public Result<bool> AddMultimediaFile(int projectId, string multiFile)
        {
            var result = new Result<bool>();
            var context = new CrowDoDbContext();

            //file.MultimediaFile = data;

            var project = context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId);

            project.MultimediaFiles.Add(new ProjectMedia() { FileName = multiFile });


            if (context.SaveChanges() < 1)  //** vaalidation for Savechanges : registration is ok or not
            {
                result.ErrorCode = 7;
                result.ErrorText = "No save";
                return result;
            }

            result.Data = true;
            return result;


        }

        public Result<bool> AddRewardPackage(int userId, int projectId, string packageName, string rewardName, decimal price)
        {
            var result = new Result<bool>();
            var context = new CrowDoDbContext();

            var rewardPackage = new RewardPackage()
            {
                PackageName = packageName,
                RewardName = rewardName,
                Price = price
            };

            var project = context.Set<Project>()
            .Where(c => c.ProjectId == projectId)
            .SingleOrDefault();

            if (project == null)
            {
                result.ErrorCode = 22;
                result.ErrorText = "No project was found";
                return result;
            }

            if (price == 0.0M)
            {
                result.ErrorCode = 10;
                result.ErrorText = "You have to give a Price";
                return result;
            }

            if (price > project.ProjectGoal)
            {
                result.ErrorCode = 10;
                result.ErrorText = "Package price shouldn't exceed Goal amount!";
                return result;
            }

            if (packageName == null)
            {
                result.ErrorCode = 11;
                result.ErrorText = "You have to give a Name to PackageReward";
                return result;
            }

            if (rewardName == null)
            {
                result.ErrorCode = 12;
                result.ErrorText = "You have to give a Reward";
                return result;
            }

            context.Add(rewardPackage);

            project.RewardPackages = new List<RewardPackage>
            {
                rewardPackage
            };

            if (context.SaveChanges() < 1)
            {
                result.ErrorCode = 7;
                result.ErrorText = "An error occurred while saving data";
                return result;
            }

            return result;

        }

        public Result<bool> DeleteProject(int userId, int projectId)
        {
            var result = new Result<bool>();
            var context = new CrowDoDbContext();


            var project = context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId);
            var user = context.Set<User>().SingleOrDefault(u => u.UserId == userId);

            user.CreatedProjects.Remove(project);


            context.Remove(project);


            user.CreatedProjectsCount--;

            if (context.SaveChanges() < 1)  //** vaalidation for Savechanges : registration is ok or not
            {
                result.ErrorCode = 7;
                result.ErrorText = "No save";
                return result;
            }



            return result;

        }

        public Result<bool> DeleteRewardPackage(int userId, int projectId, int rewardPackageId)
        {
            var result = new Result<bool>();
            var context = new CrowDoDbContext();

            var reward = context.Set<RewardPackage>().SingleOrDefault(u => u.RewardPackageId == rewardPackageId);
            var project = context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId);

            project.RewardPackages.Remove(reward);
            context.Remove(reward);

            if (context.SaveChanges() < 1)  //** vaalidation for Savechanges : registration is ok or not
            {
                result.ErrorCode = 7;
                result.ErrorText = "No save";
                return result;
            }



            return result;
        }

        public Result<bool> EditProject(int projectId, string NewProjectName, string newProjectCategory, string description,
            decimal newProjectGoal, DateTime monthDuration, int estimatedMonthDuration)
        {
            var result = new Result<bool>();
            var context = new CrowDoDbContext();
            var updateProject = context.Set<Project>().SingleOrDefault(b => b.ProjectId == projectId);

            updateProject.ProjectName = NewProjectName;
            updateProject.ProjectCategory = newProjectCategory;
            updateProject.ProjectGoal = newProjectGoal;
            updateProject.Description = description;
            updateProject.ExpirationDate = monthDuration;
            updateProject.EstimatedDurationInMonths = DateTime.Now.AddMonths(estimatedMonthDuration);

            if (newProjectGoal <= updateProject.Funds)
            {
                updateProject.IsSuccessful = true;
            }


            if (context.SaveChanges() < 1)  //** vaalidation for Savechanges : registration is ok or not
            {
                result.ErrorCode = 7;
                result.ErrorText = "No save";
                return result;
            }


            return result;
        }

        public Result<string> GetFinancialProgress(int projectId)
        {
            var context = new CrowDoDbContext();
            var result = new Result<string>();
            var project = context.Set<Project>().SingleOrDefault(i => i.ProjectId == projectId);

            if (project == null)
            {
                result.ErrorCode = 404;
                result.ErrorText = $"No project found with id :{projectId}";

                return result;
            }

            var financialProgerss = (100 * project.Funds) / project.ProjectGoal;

            var p = Convert.ToInt32(financialProgerss); //** bug gt strogulopoiei pros ta panw

            result.Data = $"{p}%";

            return result;


        } //** an tha valw lista

        public Result<bool> StatusUpdate(int userId, int projectId, string updateText)
        {
            var result = new Result<bool>();
            var context = new CrowDoDbContext();
            var project = context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId);

            project.StatusUpdate = updateText;


            if (context.SaveChanges() < 1)  //** vaalidation for Savechanges : registration is ok or not
            {
                result.ErrorCode = 7;
                result.ErrorText = "No save";
                return result;
            }

            return result; //** ! propertie
        }
    }
}
