using QGrid.Tests.Data;

internal static class SeedData
{
    internal static List<Employee> Employees = new List<Employee>
    {
        // TechCorp - 7 zaposlenika
        new Employee { Name = "John Doe", CompanyId = 1, Age = 29, Salary = 50000, SalaryFloat = 50000.5f, JoinDate = DateTime.Now.AddMonths(-12), IsActive = true },
        new Employee { Name = "Emily Carter", CompanyId = 1, Age = 32, Salary = 55000, SalaryFloat = 55000.5f, JoinDate = DateTime.Now.AddMonths(-24), IsActive = true },
        new Employee { Name = "James Smith", CompanyId = 1, Age = 25, Salary = 48000, SalaryFloat = 48000.5f, JoinDate = DateTime.Now.AddMonths(-6), IsActive = false },
        new Employee { Name = "Ava Brown", CompanyId = 1, Age = 38, Salary = 60000, SalaryFloat = 60000.5f, JoinDate = DateTime.Now.AddMonths(-36), IsActive = true },
        new Employee { Name = "Mia Davis", CompanyId = 1, Age = 28, Salary = 52000, SalaryFloat = 52000.5f, JoinDate = DateTime.Now.AddMonths(-18), IsActive = true },
        new Employee { Name = "Jackson White", CompanyId = 1, Age = 40, Salary = 63000, SalaryFloat = 63000.5f, JoinDate = DateTime.Now.AddMonths(-30), IsActive = true },
        new Employee { Name = "Amelia Harris", CompanyId = 1, Age = 34, Salary = 57000, SalaryFloat = 57000.5f, JoinDate = DateTime.Now.AddMonths(-10), IsActive = false },

        // HealthPlus - 6 zaposlenika
        new Employee { Name = "Sophia Lee", CompanyId = 2, Age = 28, Salary = 45000, SalaryFloat = 45000.5f, JoinDate = DateTime.Now.AddMonths(-18), IsActive = true },
        new Employee { Name = "Liam Martin", CompanyId = 2, Age = 35, Salary = 52000, SalaryFloat = 52000.5f, JoinDate = DateTime.Now.AddMonths(-30), IsActive = true },
        new Employee { Name = "Noah Garcia", CompanyId = 2, Age = 42, Salary = 59000, SalaryFloat = 59000.5f, JoinDate = DateTime.Now.AddMonths(-5), IsActive = false },
        new Employee { Name = "Isabella Wilson", CompanyId = 2, Age = 37, Salary = 54000, SalaryFloat = 54000.5f, JoinDate = DateTime.Now.AddMonths(-15), IsActive = true },
        new Employee { Name = "Oliver Young", CompanyId = 2, Age = 33, Salary = 50000, SalaryFloat = 50000.5f, JoinDate = DateTime.Now.AddMonths(-22), IsActive = true },
        new Employee { Name = "Charlotte King", CompanyId = 2, Age = 30, Salary = 47000, SalaryFloat = 47000.5f, JoinDate = DateTime.Now.AddMonths(-8), IsActive = true },

        // GreenEnergy Solutions - 5 zaposlenika
        new Employee { Name = "Lucas Walker", CompanyId = 3, Age = 27, Salary = 53000, SalaryFloat = 53000.5f, JoinDate = DateTime.Now.AddMonths(-13), IsActive = true },
        new Employee { Name = "Olivia Wilson", CompanyId = 3, Age = 33, Salary = 55000, SalaryFloat = 55000.5f, JoinDate = DateTime.Now.AddMonths(-20), IsActive = true },
        new Employee { Name = "Benjamin Harris", CompanyId = 3, Age = 30, Salary = 46000, SalaryFloat = 46000.5f, JoinDate = DateTime.Now.AddMonths(-25), IsActive = false },
        new Employee { Name = "Sophia Clark", CompanyId = 3, Age = 35, Salary = 57000, SalaryFloat = 57000.5f, JoinDate = DateTime.Now.AddMonths(-9), IsActive = true },
        new Employee { Name = "Mason Lewis", CompanyId = 3, Age = 32, Salary = 51000, SalaryFloat = 51000.5f, JoinDate = DateTime.Now.AddMonths(-17), IsActive = true },

        // FinTech Innovators - 8 zaposlenika
        new Employee { Name = "Isabella Young", CompanyId = 4, Age = 34, Salary = 65000, SalaryFloat = 65000.5f, JoinDate = DateTime.Now.AddMonths(-15), IsActive = true },
        new Employee { Name = "Ethan Clark", CompanyId = 4, Age = 31, Salary = 60000, SalaryFloat = 60000.5f, JoinDate = DateTime.Now.AddMonths(-22), IsActive = true },
        new Employee { Name = "Harper Moore", CompanyId = 1, Age = 29, Salary = 49000, SalaryFloat = 49000.5f, JoinDate = DateTime.Now.AddMonths(-8), IsActive = false },
        new Employee { Name = "Mason Taylor", CompanyId = 4, Age = 39, Salary = 70000, SalaryFloat = 70000.5f, JoinDate = DateTime.Now.AddMonths(-18), IsActive = true },
        new Employee { Name = "Lily Davis", CompanyId = 4, Age = 37, Salary = 63000, SalaryFloat = 63000.5f, JoinDate = DateTime.Now.AddMonths(-12), IsActive = true },
        new Employee { Name = "Daniel Scott", CompanyId = 4, Age = 42, Salary = 74000, SalaryFloat = 74000.5f, JoinDate = DateTime.Now.AddMonths(-5), IsActive = true },
        new Employee { Name = "Zoe Robinson", CompanyId = 4, Age = 27, Salary = 49000, SalaryFloat = 49000.5f, JoinDate = DateTime.Now.AddMonths(-25), IsActive = false },
        new Employee { Name = "Leo Carter", CompanyId = 4, Age = 29, Salary = 56000, SalaryFloat = 56000.5f, JoinDate = DateTime.Now.AddMonths(-11), IsActive = true },

        // RetailHub - 9 zaposlenika
        new Employee { Name = "Mason Turner", CompanyId = 5, Age = 40, Salary = 53000, SalaryFloat = 53000.5f, JoinDate = DateTime.Now.AddMonths(-10), IsActive = true },
        new Employee { Name = "Harper Johnson", CompanyId = 5, Age = 45, Salary = 58000, SalaryFloat = 58000.5f, JoinDate = DateTime.Now.AddMonths(-28), IsActive = false },
        new Employee { Name = "Ella Thompson", CompanyId = 5, Age = 30, Salary = 48000, SalaryFloat = 48000.5f, JoinDate = DateTime.Now.AddMonths(-17), IsActive = true },
        new Employee { Name = "Jacob White", CompanyId = 5, Age = 33, Salary = 52000, SalaryFloat = 52000.5f, JoinDate = DateTime.Now.AddMonths(-12), IsActive = true },
        new Employee { Name = "Aiden Harris", CompanyId = 5, Age = 29, Salary = 54000, SalaryFloat = 54000.5f, JoinDate = DateTime.Now.AddMonths(-15), IsActive = true },
        new Employee { Name = "Scarlett Miller", CompanyId = 5, Age = 32, Salary = 60000, SalaryFloat = 60000.5f, JoinDate = DateTime.Now.AddMonths(-8), IsActive = false },
        new Employee { Name = "David Lee", CompanyId = 5, Age = 38, Salary = 55000, SalaryFloat = 55000.5f, JoinDate = DateTime.Now.AddMonths(-9), IsActive = true },
        new Employee { Name = "Avery Garcia", CompanyId = 5, Age = 26, Salary = 47000, SalaryFloat = 47000.5f, JoinDate = DateTime.Now.AddMonths(-3), IsActive = true },
        new Employee { Name = "Carter Moore", CompanyId = 5, Age = 41, Salary = 62000, SalaryFloat = 62000.5f, JoinDate = DateTime.Now.AddMonths(-20), IsActive = false }
    };

    internal static List<Company> Companies = new List<Company>
    {
        new Company
        {
            Name = "TechCorp",
            JoinDate = DateTime.Now.AddMonths(-24),
            Code = 11,
            DoubleCode = 11.11,
            FloatCode = 11.11f,
            IsActive = true,
            DecimalCode = 11.111m,
            Address = new Address
            {
                Street = "123 Tech Street",
                City = "Techville",
                State = "California",
                PostalCode = "94016",
                Country = "USA"
            }
        },
        new Company
        {
            Name = "HealthPlus",
            JoinDate = DateTime.Now.AddMonths(-30),
            Code = 22,
            DoubleCode = 22.22,
            FloatCode = 22.22f,
            IsActive = false,
            DecimalCode = 22.222m,
            Address = new Address
            {
                Street = "456 Wellness Way",
                City = "Healthtown",
                State = "New York",
                PostalCode = "10001",
                Country = "USA"
            }
        },
        new Company
        {
            Name = "GreenEnergy Solutions",
            JoinDate = DateTime.Now.AddMonths(-25),
            Code = 33,
            DoubleCode = 33.33,
            FloatCode = 33.33f,
            IsActive = true,
            DecimalCode = 33.333m,
            Address = new Address
            {
                Street = "789 Eco Blvd",
                City = "Greenfield",
                State = "Oregon",
                PostalCode = "97035",
                Country = "USA"
            }
        },
        new Company
        {
            Name = "FinTech Innovators",
            JoinDate = DateTime.Now.AddMonths(-22),
            Code = 44,
            DoubleCode = 44.44,
            FloatCode = 44.44f,
            IsActive = true,
            DecimalCode = 44.444m,
            Address = new Address
            {
                Street = "101 Finance Ave",
                City = "Moneytown",
                State = "Illinois",
                PostalCode = "60605",
                Country = "USA"
            }
        },
        new Company
        {
            Name = "RetailHub",
            JoinDate = DateTime.Now.AddMonths(-28),
            Code = 55,
            DoubleCode = 55.55,
            FloatCode = 55.55f,
            IsActive = true,
            DecimalCode = 55.555m,
            Address = new Address
            {
                Street = "202 Market St",
                City = "Shophaven",
                State = "Texas",
                PostalCode = "75001",
                Country = "USA"
            }
        },
    };



    internal static void Seed(InMemoryDb context)
    {
        if (!context.Companies.Any())
        {
            // Dodaj adrese za kompanije
            var addresses = Companies.Select(c => c.Address).ToList();

            // Dodaj kompanije i adrese u kontekst
            context.Addresses.AddRange(addresses); // Dodavanje adresa
            context.Companies.AddRange(Companies); // Dodavanje kompanija
            context.Employees.AddRange(Employees); // Dodavanje zaposlenika

            // Spremanje promjena
            context.SaveChanges();
        }
    }

}
