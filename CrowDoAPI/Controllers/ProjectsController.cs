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

        public ProjectsController(IDashboardService dashboardService_)
        {
            this.dashboardService_ = dashboardService_;
        }
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>..

        private IUserService userService_;

        public ProjectsController(IUserService userService_)
        {
            this.userService_ = userService_;
        }

        ////>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>.

        private IProjectService projectService_;
        public ProjectsController(IProjectService projectService_)
        {
            this.projectService_ = projectService_;
        }

        /////>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>.
        private IReportingService reportingService_;

        public ProjectsController(IReportingService reportingService_)
        {
            this.reportingService_ = reportingService_;
        }

        ////>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>.

        private ICentralService centralService_;

        public ProjectsController(ICentralService centralService_)
        {
            this.centralService_ = centralService_;
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>.

        // GET api/values
        [HttpGet("/IDashBoardService/Projects/projectId:{projectId}/FinancialProgress")]
        public IActionResult Get(int projectId)
        {
            var result = dashboardService_.GetFinancialProgress(projectId);
            return Ok(result.Data);
        }

        // GET api/projects
        [HttpGet]
        public void DeadlineCheck()
        {
            
            projectService_.DeadlineCheck();
        }

        // GET api/projects
        [HttpGet("/IReportingService/Users/Projects/TopCreators")]
        public ActionResult<Result<List<User>>> GetTopCreators()
        {
            var result = reportingService_.GetTopCreators();
            return result;
        }

        [HttpGet("/IReportingService/Users/Projects/RecentProjects")]
        public ActionResult<Result<List<Project>>> GetRecentProjects()
        {
            var result = reportingService_.GetRecentProjects();
            return result;
        }

        [HttpGet("/IReportingService/Users/Projects/PopularProjects")]
        public ActionResult<Result<List<Project>>> GetPopularProjects()
        {
            var result = reportingService_.GetPopularProjects();
            return result;
        }

        [HttpGet("/IProjectService/Users/Projects/SuccessfulProjects")]

        public ActionResult<Result<List<Project>>> GetFundedProjects()
        {

            var result = projectService_.GetFundedProjects();

            return result;

        }

        // GET 
        [HttpGet("/IProjectService/Users/Projects/ProjectId:{projectId}/ProjectDetails")]
        public ActionResult<Result<Project>> GetProjectDetails(int projectId)
        {
            var result= projectService_.GetProjectDetails(projectId);
            return result;
        }

        // GET api/projects/5
        [HttpGet("/IProjectService/User/UserName:{CreatorName}/Projects")]
        public ActionResult<Result<List<Project>>> GetProjectByCreator(string creatorName)
        {
           var result = projectService_.SearchByCreator(creatorName);
            return result;
        }

        [HttpGet("/IProjectService/Project/SearchByText:{text}")]
        public ActionResult<Result<List<Project>>> GetProjectByText(string text)
        {
            var result = projectService_.SearchByText(text);
            return result;
        }

        // GET 
        [HttpGet("/IProjectService/Project/SearchByCategory:{category}")]
        public ActionResult<Result<List<Project>>> GetProjectByCategory(string category)
        {
            var result = projectService_.SearchByCategory(category);
            return result;
        }

        // GET 
        [HttpGet("/IProjectService/Project/SearchByYear:{year}")]
        public ActionResult<Result<List<Project>>> GetProjectByYear(int year)
        {
            var result = projectService_.SearchByYear(year);

            return result;
        }

        [HttpGet("/IProjectService/Project/PendingProjects")]
        public ActionResult<Result<List<Project>>> GetPendingProjects()
        {
            var result = projectService_.GetPendingProjects();

            return result;
        }
        // POST api/projects

        [HttpPost("/IProjectService/Users/UsersProject/UsersIdfund/")]
        public ActionResult<Result<bool>> FundProject(int userId, int projectId, int rewardPackageId)
        {
            var result = projectService_.FundProject(userId, projectId

            , rewardPackageId);

            return result;
        }


        [HttpPost("/ICentralService/ImportUsers")]
        public ActionResult<Result<bool>> ImportUsers([FromBody] ProjectMediaOptions options)
        {
            var result = centralService_.ImportUsers(options.FileName);

            return result;
        }

        [HttpPost("/ICentralService/ImportProjects")]
        public ActionResult<Result<bool>> ImportProjects([FromBody] ProjectMediaOptions options)
        {
            var result = centralService_.ImportProject(options.FileName);

            return result;
        }

        // POST api/projects/5
        [HttpPost("/IProjectService/Users/UserEmail:{creatorEmail}/PublihProject")]
        public ActionResult<Result<Project>> PostNewProject(string creatorEmail, [FromBody] ServiceProjectOptionPublish package)
        {
            var projectService = new ProjectService();

            return projectService.PublishProject(creatorEmail, package.name
            , package.category, package.description, package.projectGoal
            , package.expirationInMonths, package.creationDate, package.estimatedDurationInMonths);

        }

        // POST api/projects/5
        //[HttpPost("addPackage/{userId}/{projectId}")]
        //public ActionResult<Result<bool>> AddRewardPackage(int userId, int projectId, [FromBody] RewardPackage package)
        //{
        //    var dashboardService = new DashboardService();

        //    return dashboardService.AddRewardPackage(userId, projectId, package.PackageName, package.RewardName, package.Price);
        //}

      

        // POST api/values
        [HttpPost("/IDashBoardService/Users/UserId:{userId}/Project/ProjectId:{projectId}/AddRewardPackage")]
        public void Post(int userId, int projectId, [FromBody] RewardPackageOptions reward)
        {

            dashboardService_.AddRewardPackage(userId, projectId, reward.PackageName,
                reward.RewardName, reward.Price);

        }

        [HttpPost("/IDashBoardService/Project/ProjectId:{projectId}/GiveMediafile")]
        public void Post(int projectId, [FromBody] ProjectMediaOptions projectMedia)
        {

            dashboardService_.AddMultimediaFile(projectId, projectMedia.FileName);

        }

        [HttpPost("/IDashBoardService/Project/ProjectId:{projectId}/EditProject")]
        public void Post(int projectId, [FromBody] ProjectOptionsEdit project)
        {

            dashboardService_.EditProject(projectId, project.ProjectName,
                project.ProjectCategory, project.Description, project.ProjectGoal,
                project.ExpirationDate, project.EstimatedDurationInMonths);

        }

        [HttpPost("/IDashBoardService/Users/UsersId:{userId}/Project/ProjectId:{projectId}/StatusUpdate")]
        public void Post(int userId, int projectId, [FromBody] StatusUpdateOptions update)
        {

            dashboardService_.StatusUpdate(userId, projectId, update.Text);

        }

        [HttpPost("/IDashBoardService/Users/UsersId:{userId}/Project/ProjectId:{projectId}/RewardPackages/PewardPackageId:{rewardPackageId}/Delete")]
        public void Post(int userId, int projectId, int rewardPackageId)
        {

            dashboardService_.DeleteRewardPackage(userId, projectId, rewardPackageId);

        }

        [HttpPost("/IDashBoardService/Users/UsersId:{userId}/Project/ProjectId:{projectId}/DeleteProject")]
        public void Post(int userId, int projectId)
        {

            dashboardService_.DeleteProject(userId, projectId);

        }

        // POST api/projects
        [HttpPost("/IUserService/Users/Register")]
        public ActionResult<Result<User>> CreateUser( [FromBody] UsersServiceOptionsRegister edit)
        {
            var result = userService_.Create(edit.Email, edit.Name, edit.Address, edit.BirthDate);

            return result;
        }

        // PUT api/projects/5
        [HttpPut("/IUserService/Users/UserEmail:{userEmail}/EditAccount")]
        public ActionResult<Result<bool>> EditUser(string userEmail, [FromBody] UserServiceOptionsEditAccount edit)
        {
            var result = userService_.Edit(userEmail, edit.Name, edit.Address, edit.BirthDate);
            return result;
        }

        // DELETE api/projects/5
        [HttpDelete("/IUserService/Users/UserEmail:{userEmail}/DeleteAccount")]
        public ActionResult<Result<bool>> DeleteUser(string userEmail)
        {
            var result = userService_.Delete(userEmail);
            return result;

        }
    }
}

