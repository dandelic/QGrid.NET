namespace QGrid.Tests.Data
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public long CompanyId { get; set; }
        public Company Company { get; set; } = null!;
        public int Age { get; set; }
        public double Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public float SalaryFloat { get; set; }
        public decimal SalaryDecimal { get; set; }
    }

    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public int Code { get; set; }
        public double DoubleCode { get; set; }
        public float FloatCode { get; set; }
        public decimal DecimalCode { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Employee> Employees { get; set; } = [];
        public DateTime JoinDate { get; set; }
        public Address Address { get; set; } = null!;
        public int AddressId { get; set; }

    }

    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
