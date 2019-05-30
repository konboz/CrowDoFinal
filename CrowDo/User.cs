using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDo
{
    public class User 
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int CreatedProjectsCount { get; set; }

        public List<Project> CreatedProjects { get; set; }

        public User()
        {
            CreatedProjects = new List<Project>();
            CreatedProjectsCount = 0;
        }
    }
}
