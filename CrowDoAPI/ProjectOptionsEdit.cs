using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowDoAPI
{
    public class ProjectOptionsEdit
    {
        public string ProjectName { get; set; }

        public string ProjectCategory { get; set; } //*** ayto prepei na ginei dynamiko

        public decimal ProjectGoal { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int EstimatedDurationInMonths { get; set; }

        public string Description { get; set; }

    }
}
