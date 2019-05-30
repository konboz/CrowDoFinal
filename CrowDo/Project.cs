
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace CrowDo
{//ko
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

        
        //public User Creator { get; set; } //???//

        //public List<User> Backers { get; set; } //???//

        //public List<ProjectReward> ProjectRewards { get; set; }

        public string StatusUpdate { get; set; }

        public List<RewardPackage> RewardPackages { get; set; }

        public List<BackerReward> BackerRewards { get; set; }

        public List<ProjectMedia> MultimediaFiles { get; set; }

        

        public Project()
        {
            List<RewardPackage> RewardPackages = new List<RewardPackage>();
            List<BackerReward> BackerRewards = new List<BackerReward>();
            //Visits = 0;
        }

        //public Project(string projectName, ProjectCategory projectCategory, decimal projectGoal) //Boroume na to kanoume xwris constructor opws eipe o pnevmatikos
        //{
        //    ProjectName = projectName;
        //    ProjectCategory = projectCategory;
        //    ProjectGoal = projectGoal;
        //}


    }

}
