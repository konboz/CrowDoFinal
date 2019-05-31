using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrowDo
{
    public class ProjectService : IProjectService
    {
        public Result<bool> DeadlineCheck()     //Checks for expired Projects
        {
            var context = new CrowDoDbContext();
            var result = new Result<bool>();

            var expiredProjects = context.Set<Project>()
                .Where(p => p.ExpirationDate <= DateTime.Now)
                .ToList();

            foreach (Project p in expiredProjects)
            {
                p.IsAvailable = false;
            }

            if (context.SaveChanges() < 1)
            {
                result.ErrorCode = 7;
                result.ErrorText = "There was an error communicating with the server";

                return result;
            }

            return result;
        }

        public Result<bool> FundProject(int userId, int projectId, int rewardPackageId)
        {
            var result = new Result<bool>();

            var context = new CrowDoDbContext();

            var backer = context.Set<User>()
                .SingleOrDefault(c => c.UserId == userId);

            var package = context.Set<RewardPackage>()
                .SingleOrDefault(c => c.RewardPackageId == rewardPackageId);

            var project = context.Set<Project>()
                .Include(p => p.RewardPackages)
                .SingleOrDefault(p => p.ProjectId == projectId);

            if (backer == null)
            {
                result.ErrorCode = 20;
                result.ErrorText = "User not registered";

                return result;
            }

            if (package == null)
            {
                result.ErrorCode = 19;
                result.ErrorText = "Reward package not found";
                return result;
            }

            if (project == null)
            {
                result.ErrorCode = 18;
                result.ErrorText = "Project not found";
                return result;
            }

            if (project.IsAvailable == false)
            {
                result.ErrorCode = 29;
                result.ErrorText = "Project not available";
                return result;
            }

            if (!project.RewardPackages.Contains(package))
            {
                result.ErrorCode = 25;
                result.ErrorText = "The selected RewardPackage isn't available for this project";
                return result;
            }

            project.Funds += package.Price;

            if (project.Funds >= project.ProjectGoal)
            {
                project.IsSuccessful = true;
            }

            var backerReward = new LinkingTable
            {
                UserId = backer.UserId,
                RewardPackageId = package.RewardPackageId,
                ProjectId = projectId
            };
            context.Add(backerReward);

            if (context.SaveChanges() < 1)
            {
                result.ErrorCode = 7;
                result.ErrorText = "An error occurred while saving data";

                return result;
            }

            result.Data = true;
            return result;
        }

        public Result<List<Project>> GetFundedProjects()
        {
            var context = new CrowDoDbContext();
            var result = new Result<List<Project>>();

            var projectList = context.Set<Project>()
                .Where(p => p.IsSuccessful == true)
                .ToList();

            if (projectList == null)
            {
                result.ErrorCode = 22;
                result.ErrorText = "No projects were found";
                return result;
            }

            result.Data = projectList;
            return result;
        }

        public Result<List<Project>> GetPendingProjects()
        {
            var context = new CrowDoDbContext();
            var projectList = context.Set<Project>()
                .Where(p => p.IsAvailable == true)
                .Where(p => p.IsSuccessful == false)
                .ToList();

            var result = new Result<List<Project>>();

            if (projectList == null)
            {
                result.ErrorCode = 22;
                result.ErrorText = "No projects were found";
                return result;
            }

            result.Data = projectList;
            return result;
        }

        public Result<Project> GetProjectDetails(int projectId)
        {
            var context = new CrowDoDbContext();

            var project = context.Set<Project>()
                .Include(p => p.RewardPackages)
                .SingleOrDefault(p => p.ProjectId == projectId);

            var result = new Result<Project>();

            if (project == null)
            {
                result.ErrorCode = 22;
                result.ErrorText = "No project was found";
                return result;
            }

            if (project.IsAvailable == false)
            {
                result.ErrorCode = 29;
                result.ErrorText = "Project not available";
                return result;
            }

            project.Visits++;

            if (context.SaveChanges() < 1)
            {
                result.ErrorCode = 7;
                result.ErrorText = "There was an error communicating with the server";
                return result;
            }

            result.Data = project;
            return result;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            if (!email.Contains("@"))
            {
                return false;
            }

            return true;
        }

        public Result<Project> PublishProject(string creatorEmail, string projectName
            , string category, string description, decimal projectGoal, DateTime creationDate, DateTime monthDuration, int estimatedMonthDuration)
        {
            var result = new Result<Project>();

            if (IsValidEmail(creatorEmail) == false)
            {
                result.ErrorCode = 1;
                result.ErrorText = "Email is not valid";

                return result;
            }

            var context = new CrowDoDbContext();
            var creator = context.Set<User>()
                .Where(c => c.Email == creatorEmail)
                .SingleOrDefault();

            if (creator == null)
            {
                result.ErrorCode = 2;
                result.ErrorText = "User not registered";

                return result;
            }

            var project = new Project()
            {
                ProjectName = projectName,
                ProjectCategory = category,
                ProjectGoal = projectGoal,
                Description = description,
                CreationDate = creationDate,
                ExpirationDate = monthDuration,
                EstimatedDurationInMonths = DateTime.Now.AddMonths(estimatedMonthDuration),
                IsAvailable = true
            };

            creator.CreatedProjects.Add(project);
            creator.CreatedProjectsCount++;

            if (context.SaveChanges() < 1)
            {
                result.ErrorCode = 7;
                result.ErrorText = "An error occurred while saving data";

                return result;
            }

            result.Data = project;
            return result;
        }

        public Result<List<Project>> SearchByText(string name, string category)
        {
            var context = new CrowDoDbContext();

            var projectList = context.Set<Project>().Where(p => p.ProjectName.Contains(name))
                .Where(c => c.ProjectCategory.Contains(category)).ToList();

            var result = new Result<List<Project>>
            {
                Data = projectList
            };

            if (result.Data == null)
            {
                result.ErrorCode = 22;
                result.ErrorText = "Poject not Found!";
            }

            result.Data = projectList;
            return result;
        }

        public Result<List<Project>> SearchByCategory(string category)
        {
            var context = new CrowDoDbContext();
            var projectList = context.Set<Project>()
                .Where(p => p.ProjectCategory == category)
                .Where(p => p.IsAvailable == true)
                .ToList();

            var result = new Result<List<Project>>();

            if (projectList == null)
            {
                result.ErrorCode = 22;
                result.ErrorText = "No project was found";
                return result;
            }

            result.Data = projectList;
            return result;
        }

        public Result<List<Project>> SearchByCreator(string email)
        {
            var context = new CrowDoDbContext();

            var creator = context.Set<User>()
                .Include(c => c.CreatedProjects)
                .SingleOrDefault(p => p.Email == email);

            var result = new Result<List<Project>>();

            if (creator == null)
            {
                result.ErrorCode = 24;
                result.ErrorText = "No creators found";
                return result;
            }

            result.Data = creator.CreatedProjects;
            return result;
        }
     
        public Result<List<Project>> SearchByYear(int year)
        {
            var context = new CrowDoDbContext();
            var projectList = context.Set<Project>()
                .Where(p => p.CreationDate.Year == year)
                .Where(p => p.IsAvailable == true)
                .ToList();

            var result = new Result<List<Project>>();

            if (projectList == null)
            {
                result.ErrorCode = 22;
                result.ErrorText = "No project was found";
                return result;
            }

            result.Data = projectList;
            return result;
        }
    }
}

