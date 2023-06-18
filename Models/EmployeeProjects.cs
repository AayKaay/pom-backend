namespace pom_api.Models
{
    public class EmployeeProjects
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string ProjectName { get; set; }
        public string WorkDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
