using System.Net.Http;
using System.Threading.Tasks;

namespace foundry_assessment_RAZOR.Model
{
    public class EngagementModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string client { get; set; } // Client ID
        public string employee { get; set; } // employee ID
        public string description { get; set; }
        public DateTime started { get; set; }
        public Nullable<DateTime> ended { get; set; } // Can be null

    }
    public class EngagementDetails
    {
        public string id { get; set; }
        public string name { get; set; }
        public ClientClass client { get; set; }
        public EmployeeClass employee { get; set; }
        public string description { get; set; }
        public DateTime started { get; set; }
        public Nullable<DateTime> ended { get; set; } // Can be null

    }
    public class CreateEngagements
    {
        public string name { get; set; }
        public string client { get; set; }
        public string employee { get; set; }
        public string? description { get; set; }

    }

    public class UpdateEngagment
    {
        public string id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
    }
}