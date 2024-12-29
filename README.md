### What's QGrid?

**QGrid.NET** is a library designed to simplify server-side operations for filtering, sorting and pagination of `IQueryable` data sources. 
By using `LINQ` expressions and reflection it dynamically constructs and executes queries, making it easier to handle data operations for APIs and services.

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
To retrieve a filtered and paginated list of employees, we need to send a `QueryModel` object to the API endpoint.

Here's the example in JSON format:

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
      "value": "Global Company",
      "operand": "eq",
      "operator": "and"
    }
  ],
  "search" : {
    "term": "Jo",
    "operand": "sw",
    "properties": ["FirstName"]
  },
  "sort": [
    {
      "property": "FirstName",
      "ascending": true
    }
  ],
  "pagination": {
    "rows": 5,
    "page": 1
  }
}


```
By sending this filter we apply:
- `Salary > 2500` (employees earning more than 2500).
- `Company.Name == "Global Company"` (employees working for "Global Company").
- `FirstName` starts with "Jo".
- Sort by `FirstName` in ascending order.
- First page with maximum of 10 records per page.


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

Upon receiving the `QueryModel` object in the `Controller`, we use the `Evaluate` method to apply data operations on the query. 

The result is a mapped data response, complete with pagination details, wrapped in a `QPagedResponse` object.

Example of the `QPagedResponse` response in JSON format:

```json
{
    "pagination": {
        "currentPage": 1,
        "pageSize": 5,
        "totalPages": 4,
        "totalCount": 18
    },
    "data": [
        {
            "name": "John Doe",
            "salary": 3000,
            "companyName": "Global Company"
        },
        {
            "name": "Joanna Smith",
            "salary": 3500,
            "companyName": "Global Company"
        },
        {
            "name": "Johnny Depp",
            "salary": 4500,
            "companyName": "Global Company"
        },
        {
            "name": "Jordan Johnson",
            "salary": 5000,
            "companyName": "Global Company"
        },
        {
            "name": "Jodie Turner",
            "salary": 3200,
            "companyName": "Global Company"
        }
    ]
}
```


