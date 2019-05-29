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
        // GET api/projects
        [HttpGet]
        public void DeadlineCheck()
        {
            var projectService = new ProjectService();
            projectService.DeadlineCheck();
        }

        // GET api/projects
        [HttpGet("topCreators")]
        public ActionResult<Result<List<User>>> GetTopCreators()
        {
            var reportingService = new ReportingService();
            return reportingService.GetTopCreators();
        }

        [HttpGet("recentProjects")]
        public ActionResult<Result<List<Project>>> GetRecentProjects()
        {
            var reportingService = new ReportingService();
            return reportingService.GetRecentProjects();
        }

        [HttpGet("popularProjects")]
        public ActionResult<Result<List<Project>>> GetPopularProjects()
        {
            var reportingService = new ReportingService();
            return reportingService.GetPopularProjects();
        }

        [HttpGet("SuccessfulProjects")]

        public ActionResult<Result<List<Project>>> GetFundedProjects()
        {

            var projectService = new ProjectService();

            return projectService.GetFundedProjects();

        }

        // GET api/projects/5
        [HttpGet("{projectId}")]
        public ActionResult<Result<Project>> GetProjectDetails(int projectId)
        {
            var projectService = new ProjectService();

            return projectService.GetProjectDetails(projectId);
        }

        // GET api/projects/5
        [HttpGet("searchByCreator/{creatorName}")]
        public ActionResult<Result<List<Project>>> GetProjectByCreator(string creatorName)
        {
            var projectService = new ProjectService();
           
            return projectService.SearchByCreator(creatorName);
        }

        [HttpGet("searchByText/{text}")]
        public ActionResult<Result<List<Project>>> GetProjectByText(string text)
        {
            var projectService = new ProjectService();
       
            return projectService.SearchByText(text);
        }

        // GET api/projects/5
        [HttpGet("searchByCategory/{category}")]
        public ActionResult<Result<List<Project>>> GetProjectByCategory(string category)
        {
            var projectService = new ProjectService();
           
            return projectService.SearchByCategory(category);
        }

        // GET api/projects/5
        [HttpGet("searchByYear/{year}")]
        public ActionResult<Result<List<Project>>> GetProjectByYear(int year)
        {
            var projectService = new ProjectService();
     
            return projectService.SearchByYear(year);
        }

        [HttpGet("pendingProjects")]
        public ActionResult<Result<List<Project>>> GetPendingProjects()
        {
            var projectService = new ProjectService();

            return projectService.GetPendingProjects();
        }

        public struct Package
        {
            public string name;
            public string category;
            public string description;
            public decimal projectGoal;
            public DateTime expirationInMonths;
            public DateTime creationDate;
            public int estimatedDurationInMonths;
        }

        //public struct Packet
        //{

        //    public int projectId;
        //    public int rewardPackageId;
        //}

        // POST api/projects

        [HttpPost("fund/{userId}")]
        public ActionResult<Result<bool>> FundProject(int userId, int projectId, int rewardPackageId)
        {
            var projectService = new ProjectService();

            return projectService.FundProject(userId, projectId
            , rewardPackageId);
        }
        
        // POST api/projects/5
        [HttpPost("publish/{creatorEmail}")]
        public ActionResult<Result<Project>> PostNewProject(string creatorEmail, [FromBody] Package package)
        {
            var projectService = new ProjectService();

            return projectService.PublishProject(creatorEmail, package.name
            , package.category, package.description, package.projectGoal
            , package.expirationInMonths, package.creationDate, package.estimatedDurationInMonths);

        }

        // POST api/projects/5
        [HttpPost("addPackage/{userId}/{projectId}")]
        public ActionResult<Result<bool>> AddRewardPackage(int userId, int projectId, [FromBody] RewardPackage package)
        {
            var dashboardService = new DashboardService();

            return dashboardService.AddRewardPackage(userId, projectId, package.PackageName, package.RewardName, package.Price);
        }

        public struct Edits
        {
            public string Name;
            public string Address;
            public DateTime BirthDate;
        }
        
        // POST api/projects
        [HttpPost("/api/users/create/{email}")]
        public ActionResult<Result<User>> CreateUser(string email, [FromBody] Edits edit)
        {
            var userService = new UserService();
            return userService.Create(email, edit.Name, edit.Address, edit.BirthDate);
        }


        // PUT api/projects/5
        [HttpPut("/api/users/update/{email}")]
        public ActionResult<Result<bool>> EditUser(string email, [FromBody] Edits edit)
        {
            var userService = new UserService();
            return userService.Edit(email, edit.Name, edit.Address, edit.BirthDate);
        }

        // DELETE api/projects/5
        [HttpDelete("/api/users/delete/{email}")]
        public ActionResult<Result<bool>> DeleteUser(string email)
        {
            var userService = new UserService();
            return userService.Delete(email);

        }
    }
}

