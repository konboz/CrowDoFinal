using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDo
{
    public class ImportProject
    {
        public string nameOfProject { get; set; }
        public string description { get; set; }
        public string creator { get; set; }
        public string keywords { get; set; }
        public decimal demandedfunds { get; set; }
        public DateTime dateOfCreation { get; set; }
        public DateTime deadline { get; set; }
        public int estimatedDurationInMonths { get; set; }


    }


    public class ImportUsers
    {
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string personalMoto { get; set; }
    }
}
