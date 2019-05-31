using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDo;

namespace CrowDoAPI.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        private IDashboardService dashboardService_;
        private IUserService userService_;
        private IProjectService projectService_;
        private IReportingService reportingService_;
        private ICentralService centralService_;



        public ProjectsController(IDashboardService dashboardService_, IUserService userService_,
            IProjectService projectService_, IReportingService reportingService_, ICentralService centralService_)
        {
            this.dashboardService_ = dashboardService_;
            this.userService_ = userService_;
            this.projectService_ = projectService_;
            this.reportingService_ = reportingService_;
            this.centralService_ = centralService_;

        }
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>..


        //GET api/projects
        [HttpGet]
        public void DeadlineCheck()
        {

            projectService_.DeadlineCheck();
        }


        //GET api/projects

        [HttpGet("/Reports/Projects/RecentProjects")]
        public ActionResult<Result<List<Project>>> GetRecentProjects()
        {
            var result = reportingService_.GetRecentProjects();
            return result;
        }

        [HttpGet("/Reports/Projects/PopularProjects")]
        public ActionResult<Result<List<Project>>> GetPopularProjects()
        {
            var result = reportingService_.GetPopularProjects();
            return result;
        }

        [HttpGet("/Projects/SuccessfulProjects")]
        public ActionResult<Result<List<Project>>> GetFundedProjects()
        {
            var result = projectService_.GetFundedProjects();
            return result;
        }

        [HttpGet("/Projects/PendingProjects")]
        public ActionResult<Result<List<Project>>> GetPendingProjects()
        {
            var result = projectService_.GetPendingProjects();

            return result;
        }

        [HttpGet("/Reports/Projects/TopCreators")]
        public ActionResult<Result<List<User>>> GetTopCreators()
        {
            var result = reportingService_.GetTopCreators();
            return result;
        }

        // GET api/projects/5

        [HttpGet("/Projects/ProjectId:{projectId}/ProjectDetails")]
        public ActionResult<Result<Project>> GetProjectDetails(int projectId)
        {
            var result = projectService_.GetProjectDetails(projectId);
            return result;
        }

        [HttpGet("/DashBoard/Projects/projectId:{projectId}/FinancialProgress")]
        public IActionResult Get(int projectId)
        {
            var result = dashboardService_.GetFinancialProgress(projectId);
            return Ok(result.Data);
        }

        [HttpGet("/Project/User/UserEmail:{email}/Projects/SearchByCreator")]
        public ActionResult<Result<List<Project>>> GetProjectByCreator(string email)
        {
            var result = projectService_.SearchByCreator(email);
            return result;
        }

        [HttpGet("/Projects/SearchByText{name}/{category}")]
        public ActionResult<Result<List<Project>>> GetProjectByText(string name, string category)
        {
            var result = projectService_.SearchByText(name, category);
            return result;
        }

        [HttpGet("/Projects/SearchByCategory:{category}")]
        public ActionResult<Result<List<Project>>> GetProjectByCategory(string category)
        {
            var result = projectService_.SearchByCategory(category);
            return result;
        }

        [HttpGet("/Projects/SearchByYear:{year}")]
        public ActionResult<Result<List<Project>>> GetProjectByYear(int year)
        {
            var result = projectService_.SearchByYear(year);

            return result;
        }


        // POST api/projects/5

        [HttpPost("/Operations/ImportUsers")]
        public ActionResult<Result<bool>> ImportUsers([FromBody] ProjectMediaOptions options)
        {
            var result = centralService_.ImportUsers(options.FileName);

            return result;
        }

        [HttpPost("/Operations/ImportProjects")]
        public ActionResult<Result<bool>> ImportProjects([FromBody] ProjectMediaOptions options)
        {
            var result = centralService_.ImportProject(options.FileName);

            return result;
        }

        [HttpPost("/Projects/Users/{userId}/Projects/{projectId}/{rewardPackageId}/fund")]
        public ActionResult<Result<bool>> FundProject(int userId, int projectId, int rewardPackageId)
        {
            var result = projectService_.FundProject(userId, projectId
            , rewardPackageId);

            return result;
        }

        [HttpPost("/Projects/Users/UserEmail:{creatorEmail}/PublishProject")]
        public ActionResult<Result<Project>> PostNewProject(string creatorEmail, [FromBody] ServiceProjectOptionPublish package)
        {
            var result = projectService_.PublishProject(creatorEmail, package.name
            , package.category, package.description, package.projectGoal
            , package.expirationInMonths, package.creationDate, package.estimatedDurationInMonths);

            return result;

        }

        // POST api/Users/...

        [HttpPost("/DashBoard/Users/UserId:{userId}/Project/ProjectId:{projectId}/AddRewardPackage")]
        public ActionResult<Result<bool>> Post(int userId, int projectId, [FromBody] RewardPackageOptions reward)
        {

            var result = dashboardService_.AddRewardPackage(userId, projectId, reward.PackageName,
                reward.RewardName, reward.Price);

            return result;
        }

        [HttpPost("/DashBoard/Users/UserId:{userId}/Project/ProjectId:{projectId}/GiveMediafile")]
        public ActionResult<Result<bool>> Post(int userId, int projectId, [FromBody] ProjectMediaOptions projectMedia)
        {
            var result = dashboardService_.AddMultimediaFile(userId, projectId, projectMedia.FileName);

            return result;
        }
        
        [HttpPost("/DashBoard/Users/UsersId:{userId}/Project/ProjectId:{projectId}/StatusUpdate")]
        public ActionResult<Result<bool>> Post(int userId, int projectId, [FromBody] StatusUpdateOptions update)
        {
            var result = dashboardService_.StatusUpdate(userId, projectId, update.Text);

            return result;
        }


        // POST api/Users/...

        [HttpPost("/Users/Register")]
        public ActionResult<Result<User>> CreateUser([FromBody] UsersServiceOptionsRegister edit)
        {
            var result = userService_.Create(edit.Email, edit.Name, edit.Address, edit.BirthDate);

            return result;
        }


        // PUT api/Users/...

        [HttpPut("/Users/UserEmail:{userEmail}/EditAccount")]
        public ActionResult<Result<bool>> EditUser(string userEmail, [FromBody] UserServiceOptionsEditAccount edit)
        {
            var result = userService_.Edit(userEmail, edit.Name, edit.Address, edit.BirthDate);
            return result;
        }

        [HttpPut("/DashBoard/Users/UserId:{userId}/Project/ProjectId:{projectId}/EditProject")]
        public ActionResult<Result<bool>> EditProject(int userId, int projectId, [FromBody] ProjectOptionsEdit project)
        {
            var result = dashboardService_.EditProject(userId, projectId, project.ProjectName,
                project.ProjectCategory, project.Description, project.ProjectGoal,
                project.ExpirationDate, project.EstimatedDurationInMonths);

            return result;
        }


        // DELETE api/Users/...

        [HttpDelete("/Users/UserEmail:{userEmail}/DeleteAccount")]
        public ActionResult<Result<bool>> DeleteUser(string userEmail)
        {
            var result = userService_.Delete(userEmail);
            return result;
        }

        [HttpDelete("/DashBoard/Users/UsersId:{userId}/Project/ProjectId:{projectId}/RewardPackages/RewardPackageId:{rewardPackageId}/Delete")]
        public ActionResult<Result<bool>> DeleteUser(int userId, int projectId, int rewardPackageId)
        {
            var result = dashboardService_.DeleteRewardPackage(userId, projectId, rewardPackageId);
            return result;
        }

        [HttpDelete("/DashBoard/Users/UsersId:{userId}/Project/ProjectId:{projectId}/DeleteProject")]
        public ActionResult<Result<bool>> DeleteProject(int userId, int projectId)
        {
            var result = dashboardService_.DeleteProject(userId, projectId);

            return result;
        }
    }
}

