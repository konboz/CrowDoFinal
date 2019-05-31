using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CrowDo
{
    public class ReportingService : IReportingService
    {
        public Result<List<Project>> GetPopularProjects()
        {
            var context = new CrowDoDbContext();
            var result = new Result<List<Project>>();

            var popularProjects = context.Set<Project>()
                .OrderByDescending(p => p.Visits)
                .Take(5)
                .ToList();

            result.Data = popularProjects;
            return result;
        }

        public Result<List<Project>> GetRecentProjects()
        {
            var context = new CrowDoDbContext();
            var result = new Result<List<Project>>();

            var recentProjects = context.Set<Project>()
                .OrderByDescending(p => p.CreationDate)
                .Take(5)
                .ToList();

            result.Data = recentProjects;
            return result;

        }

        public Result<List<User>> GetTopCreators()
        {
            var context = new CrowDoDbContext();
            var result = new Result<List<User>>();

            var topCreators = context.Set<User>().Where(u => u.CreatedProjectsCount > 0)
                .OrderByDescending(u => u.CreatedProjectsCount)
                .Take(20)
                .ToList();

            result.Data = topCreators;
            return result;
        }

        public Result<bool> MonthlyReport(string ExcelFileName)
        {
            var context = new CrowDoDbContext();
            var result = new Result<bool>();
            var monthlyProjects = context.Set<Project>()
                .Include(p => p.RewardPackages)
                  .Where(p => p.CreationDate.AddDays(30) >= DateTime.Today)
                  .ToList();

            XSSFWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet("Mysheet");

            var row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("ProjectId");
            row.CreateCell(1).SetCellValue("ProjectName");
            row.CreateCell(2).SetCellValue("Description");
            row.CreateCell(3).SetCellValue("ProjectCategory");
            row.CreateCell(4).SetCellValue("CreationDate");
            row.CreateCell(5).SetCellValue("ExpirationDate");
            row.CreateCell(6).SetCellValue("ProjectGoal");
            row.CreateCell(7).SetCellValue("Funds");
            row.CreateCell(8).SetCellValue("RewardPackages");
            row.CreateCell(9).SetCellValue("IsAvailable");
            row.CreateCell(10).SetCellValue("IsSuccessful");
            row.CreateCell(11).SetCellValue("EstimatedDurationInMonths");

            for (int i = 0; i < monthlyProjects.Count; i++)
            {
                row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(monthlyProjects[i].ProjectId);
                row.CreateCell(1).SetCellValue(monthlyProjects[i].ProjectName);
                row.CreateCell(2).SetCellValue(monthlyProjects[i].Description);
                row.CreateCell(3).SetCellValue(monthlyProjects[i].ProjectCategory);
                row.CreateCell(4).SetCellValue(monthlyProjects[i].CreationDate.ToShortDateString());
                row.CreateCell(5).SetCellValue(monthlyProjects[i].ExpirationDate.ToShortDateString());
                row.CreateCell(6).SetCellValue(monthlyProjects[i].ProjectGoal.ToString());
                row.CreateCell(7).SetCellValue(monthlyProjects[i].Funds.ToString());
                row.CreateCell(8).SetCellValue(monthlyProjects[i].RewardPackages.Count.ToString());
                row.CreateCell(9).SetCellValue(monthlyProjects[i].IsAvailable);
                row.CreateCell(10).SetCellValue(monthlyProjects[i].IsSuccessful);
                row.CreateCell(11).SetCellValue(monthlyProjects[i].EstimatedDurationInMonths);
            }
            using (var fs = new FileStream(ExcelFileName, FileMode.Create,
           FileAccess.Write))
            {
                wb.Write(fs);
            }

            return result;
        }

        public Result<bool> WeeklyReport(string ExcelFileName)
        {
            var context = new CrowDoDbContext();
            var result = new Result<bool>();
            var weeklyProjects = context.Set<Project>()
                  .Where(p => p.CreationDate.AddDays(7) >= DateTime.Today)
                  .ToList();
            XSSFWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet("Mysheet");

            var row = sheet.CreateRow(0);

            row.CreateCell(0).SetCellValue("ProjectId");
            row.CreateCell(1).SetCellValue("ProjectName");
            row.CreateCell(2).SetCellValue("Description");
            row.CreateCell(3).SetCellValue("ProjectCategory");
            row.CreateCell(4).SetCellValue("CreationDate");
            row.CreateCell(5).SetCellValue("ExpirationDate");
            row.CreateCell(6).SetCellValue("ProjectGoal");
            row.CreateCell(7).SetCellValue("Funds");
            row.CreateCell(8).SetCellValue("RewardPackages");
            row.CreateCell(9).SetCellValue("IsAvailable");
            row.CreateCell(10).SetCellValue("IsSuccessful");
            row.CreateCell(11).SetCellValue("EstimatedDurationInMonths");

            for (int i = 0; i < weeklyProjects.Count; i++)
            {
                row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(weeklyProjects[i].ProjectId);
                row.CreateCell(1).SetCellValue(weeklyProjects[i].ProjectName);
                row.CreateCell(2).SetCellValue(weeklyProjects[i].Description);
                row.CreateCell(3).SetCellValue(weeklyProjects[i].ProjectCategory.ToString());
                row.CreateCell(4).SetCellValue(weeklyProjects[i].CreationDate.ToShortDateString());
                row.CreateCell(5).SetCellValue(weeklyProjects[i].ExpirationDate.ToShortDateString());
                row.CreateCell(6).SetCellValue(weeklyProjects[i].ProjectGoal.ToString());
                row.CreateCell(7).SetCellValue(weeklyProjects[i].Funds.ToString());
                row.CreateCell(8).SetCellValue(weeklyProjects[i].RewardPackages.ToString());
                row.CreateCell(9).SetCellValue(weeklyProjects[i].IsAvailable);
                row.CreateCell(10).SetCellValue(weeklyProjects[i].IsSuccessful);
                row.CreateCell(11).SetCellValue(weeklyProjects[i].EstimatedDurationInMonths);
            }
            using (var fs = new FileStream(ExcelFileName, FileMode.Create,
          FileAccess.Write))
            {
                wb.Write(fs);
            }
            return result;
        }

    }
}
