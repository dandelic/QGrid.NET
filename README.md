### What's QGrid?

**QGrid.NET** is a .NET library designed to simplify server-side operations for filtering, sorting and pagination of `IQueryable` data sources. 
By using `LINQ` and reflection it dynamically constructs and executes queries, making it easier to handle data operations for APIs and services.

It allows users to filter and sort by multiple properties, including nested properties. Conditions are combined using `AND` or `OR` operators.
Also, the library provides pagination and search functionality, allowing you to search across multiple properties simultaneously.


### How does it work?

Let's say we have a simple database model with entities **Employee** and **Company**.

```csharp
public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public ICollection<Employee> Employees { get; set; } 
}

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Salary { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}

```
To retrieve a filtered and paginated list of employees, we need to send a `QueryModel` object to the API endpoint, which includes the necessary filters, sorting, and pagination details.

Then we use `Evaluate` method that will perform data operations on query and give us mapped results alongside pagination information inside `QPagedResponse` class.

```csharp
[ApiController]
[Route("api/employees")]
public class EmployeeController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpPost]
    public IActionResult GetEmployees([FromBody] QueryModel queryModel)
    {
        IQueryable<Employee> query = _context.Employees;
     
        QPagedResponse<EmployeeDTO> result = query.Evaluate(queryModel, employee => new EmployeeDTO
        {
            Name = employee.Name,
            Salary = employee.Salary,
            CompanyName = employee.Company.Name
        });

        return Ok(result);
    }
}
```



If the JSON payload sent to the endpoint looked this we would get:
- `Salary > 2500` (employees earning more than 2500).
- `Company.Name == "Example Company"` (employees working for "Example Company").
- `FirstName` and `LastName` starts with "Jo".
- Sorted by `FirstName` in ascending order.
- First page with 10 rows.

```json
{
  "filters": [
    {
      "property": "Salary",
      "value": "2500",
      "operand": "gt",
      "operator": "and"
    },
    {
      "property": "Company.Name",
      "value": "Example Company",
      "operand": "eq",
      "operator": "and"
    }
  ],
  "search" : {
    "term": "Jo",
    "operator": "and",
    "operand": "sw",
    "properties": ["FirstName", "LastName"]
  },
  "sort": [
    {
      "property": "FirstName",
      "ascending": true
    }
  ],
  "pagination": {
    "rows": 10,
    "page": 1
  }
}
