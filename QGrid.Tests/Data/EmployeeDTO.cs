namespace QGrid.Tests.Data
{
    public class EmployeeDTO
    {
        public string Name { get; set; } = null!;
        public double Salary { get; set; }
        public int Age { get; set; }
        public string CompanyName { get; set; } = null!;
        public string CompanyAddress { get; set; } = null!;
        public double CompanyAverageSalary { get; set; }
        public int CompanyEmployeesCount { get; set; }
    }
}
