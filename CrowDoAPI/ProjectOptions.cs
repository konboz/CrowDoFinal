using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowDoAPI
{
    public class ProjectOptions
    {
        public string ProjectName { get; set; }

        public string ProjectCategory { get; set; } 

        public string Description { get; set; }

        public string StatusUpdate { get; set; }

        public decimal ProjectGoal { get; set; }

        public decimal Funds { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime EstimatedDurationInMonths { get; set; }
        public int Visits { get; set; }
       
     
    }
}
