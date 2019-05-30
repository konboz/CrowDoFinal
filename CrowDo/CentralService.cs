using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CrowDo
{
    public class CentralService : ICentralService
    {
        public Result<bool> ImportProject(string fileName)
        {
            var result = new Result<bool>();
            var projectService = new ProjectService();
            // var context = new CrowdoDbContext();


            string data = File.ReadAllText($@"{fileName}");

            var importDatas = JsonConvert.DeserializeObject<List<ImportProject>>(data);
            foreach (var importData in importDatas)
            {
                var response = projectService.PublishProject(importData.creator, importData.nameOfProject, importData.keywords, importData.description,
                    importData.demandedfunds, importData.dateOfCreation, importData.deadline, importData.estimatedDurationInMonths);
                if (response.ErrorCode != 0)
                {
                    result.ErrorCode = 7;
                    result.ErrorText = "Project not saved!Why";
                    return result;
                }
            }

            result.Data = true;
            return result;

        }

        public Result<bool> ImportUsers(string fileName)
        {
            var result = new Result<bool>();
            var userService = new UserService();



            string data = File.ReadAllText($@"{fileName}");

            var importDatas = JsonConvert.DeserializeObject<List<ImportUsers>>(data);
            foreach (var importData in importDatas)
            {
                Result<User> response = userService.Create(importData.email, importData.name, importData.address, importData.dateOfBirth);
                if (response.ErrorCode != 0)
                {
                    result.ErrorCode = 7;
                    result.ErrorText = "User not saved!";
                    return result;
                }

            }

            result.Data = true;
            return result;

        }
    }
}
