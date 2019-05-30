using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDo
{
    public class CrowDoDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=localhost;Database=CrowDo; Trusted_Connection = True; ConnectRetryCount = 0;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>();
            modelBuilder.Entity<RewardPackage>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<LinkingTable>()
                .HasKey(bp => new { bp.UserId, bp.RewardPackageId, bp.ProjectId });
        }
    }
}
