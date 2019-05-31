
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace CrowDo
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }
       
        public string Description { get; set; }

        public string ProjectCategory { get; set; }

        public decimal ProjectGoal { get; set; }

        public decimal Funds { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsSuccessful { get; set; }

        public int Visits { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime EstimatedDurationInMonths { get; set; }

        public string StatusUpdate { get; set; }

        public List<RewardPackage> RewardPackages { get; set; }

        public List<LinkingTable> LinkingTables { get; set; }

        public List<ProjectMedia> MultimediaFiles { get; set; }

        

        public Project()
        {
            List<RewardPackage> RewardPackages = new List<RewardPackage>();
            List<LinkingTable> LinkingTables = new List<LinkingTable>();
        }
    }

}
