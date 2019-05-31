using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDo
{
    public class LinkingTable
    {
        public int UserId { get; set; }
        public int RewardPackageId { get; set; }
        public User User { get; set; }
        public RewardPackage RewardPackage { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}
