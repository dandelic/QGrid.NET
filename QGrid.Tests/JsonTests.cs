using QGrid.NET.Builder;
using QGrid.NET.Builder.Models;
using QGrid.NET.Serialization.Models;
using QGrid.Tests.Data;

namespace QGrid.Tests
{
    public class JsonTests : TestBase
    {     
        [Fact]
        public void QGrid_Filter_Sort_Paginate_FromJSON_ShouldReturnCorrectResults()
        {
            string json = GetAsset("test1.json");
            var query = _context.Employees.AsQueryable();
            var queryFilter = new QueryModel(json);

            QueryBuilder<Employee> builder = new(query, queryFilter);
            QPagedResponse<EmployeeDTO> result = builder.Evaluate(employee => new EmployeeDTO
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Age = employee.Age,
                CompanyName = employee.Company.Name,
                CompanyAddress = employee.Company.Address.Street,
                CompanyAverageSalary = employee.Company.Employees.Average(e => e.Salary),
                CompanyEmployeesCount = employee.Company.Employees.Count()
            });

            Assert.All(result.Data, e => Assert.True(e.Age > 30 && e.Salary > 45000));
            Assert.True(result.Data.Zip(result.Data.Skip(1), (a, b) => string.Compare(a.Name, b.Name) <= 0).All(x => x),
                "The data is not sorted by Name in ascending order.");
        }
    }
}
