
> # If You want to run this format just you can press ctrl+shift+v



# 📘 Entity Framework Core Cheat Sheet
_For .NET Developers – Covers Queries, Raw SQL, Stored Procedures, and Best Practices_

---

## 1. IQueryable<T> vs IEnumerable<T>

| Aspect | IQueryable<T> | IEnumerable<T> |
|--------|----------------|----------------|
| **Execution** | Deferred (executed on enumeration) | Immediate (when `.AsEnumerable()` is called) |
| **Filtering** | Translated to SQL `WHERE` | Done in C# memory |
| **Performance** | ✅ Efficient (DB-level filtering) | ❌ Inefficient (loads all data first) |
| **Use Case** | Building DB queries | Processing in-memory collections |

### ✅ Good Example (IQueryable)
``` EFCore
var result = await context.Employees
.Where(e => e.Salary > 70000)
.ToListAsync();
```



### ❌ Bad Example (IEnumerable)
``` EFCore
var result = context.Employees
.AsEnumerable() // ⚠️ Forces full load!
.Where(e => e.Salary > 70000)
.ToList();
```



> 🚨 **Rule:** Never call `.AsEnumerable()` or `.ToList()` before filtering!

---

## 2. Multi-Table Queries

### Using Navigation Properties (Recommended)
``` EFCore
var employees = await context.Employees
.Where(e => e.Department.Name == "IT")
.Select(e => new { e.Name, Dept = e.Department.Name })
.ToListAsync();
```



### Using Explicit Join

``` EFCore
var employees = await (from e in context.Employees
join d in context.Departments on e.DepartmentId equals d.Id
where d.Name == "IT"
select new { e.Name, Dept = d.Name })
.ToListAsync();
```

---



---

## 3. Aggregate Functions

All support async execution and translation to SQL.

``` EFCore
int count = await context.Employees.CountAsync();
decimal total = await context.Employees.SumAsync(e => e.Salary);
double avg = await context.Employees.AverageAsync(e => e.Salary);
decimal max = await context.Employees.MaxAsync(e => e.Salary);

```


### Grouping Example
``` EFCore
var stats = await (from e in context.Employees
join d in context.Departments on e.DepartmentId equals d.Id
group e by d.Name into g
select new {
Dept = g.Key,
Count = g.Count(),
AvgSalary = g.Average(x => x.Salary)
})
.ToListAsync();
```



---

## 4. Stored Procedures & Raw SQL

### When to Use What
| Scenario | Method |
|-----------|--------|
| SP returns entities | `DbSet.FromSqlInterpolated(...)` |
| SP modifies data | `context.Database.ExecuteSqlInterpolatedAsync(...)` |
| Raw SQL update/delete | `context.Database.ExecuteSqlInterpolatedAsync(...)` |
| SP with output params | `SqlParameter + ExecuteSqlRawAsync` |

### ✅ 4.1 Query Stored Procedure (Return Entities)
``` EFCore
var employees = await context.Employees
.FromSqlInterpolated($"EXEC GetEmployeesByDepartment {deptName}")
.ToListAsync();
```



### ✅ 4.2 Execute Stored Procedure (No Return)
``` EFCore
await context.Database.ExecuteSqlInterpolatedAsync(
$"EXEC UpdateSalaryByDepartment {deptName}, {increase}"
);
```


### ✅ 4.3 Raw SQL Command
``` EFCore
await context.Database.ExecuteSqlInterpolatedAsync(
$"UPDATE Employees SET Salary = Salary * {multiplier} WHERE DepartmentId = {deptId}"
);
```



### ✅ 4.4 Stored Procedure with Output Parameter
``` EFCore
using Microsoft.Data.SqlClient;

var outputParam = new SqlParameter {
ParameterName = "@Count",
SqlDbType = SqlDbType.Int,
Direction = ParameterDirection.Output
};

await context.Database.ExecuteSqlRawAsync("EXEC GetEmployeeCount @Count OUT", outputParam);
int count = (int)outputParam.Value;

```



---

## 5. dbContext.Entity vs dbContext.Database

| Purpose | dbContext.Entity | dbContext.Database |
|----------|------------------|-------------------|
| **Focus** | Work with entities | Direct DB operations |
| **Change Tracking** | ✅ Yes | ❌ No |
| **LINQ Support** | ✅ Yes | ❌ No |
| **Returns** | `IQueryable<Employee>` | `int` (rows affected) |
| **Use For** | CRUD, queries | Raw SQL, SPs, migrations |

**Use dbContext.Employees for:**
- LINQ queries
- Tracked updates (`SaveChanges`)
- Navigation properties

**Use dbContext.Database for:**
- Stored Procedures
- Bulk operations
- Database maintenance

---

## 6. Security & Best Practices

### ✅ Always Use Interpolated Methods
// ✅ SAFE
``` EFCore
await context.Database.ExecuteSqlInterpolatedAsync(
$"UPDATE Employees SET Salary = {newSalary} WHERE Id = {id}"
);
```

// ❌ NEVER concatenate strings
``` EFCore
$"UPDATE Employees SET Salary = {userInput}" // SQL INJECTION RISK!

```


### ⚠️ Dynamic Table/Column Names
Cannot be parameterized — validate via whitelist.

``` EFCore
if (!allowedColumns.Contains(column)) throw new Exception("Invalid column");
await context.Database.ExecuteSqlRawAsync($"UPDATE Employees SET {column} = {{0}}", value);
```


### 🔄 Avoid Mixing Approaches
// After raw SQL update, reload entity to avoid stale data

``` EFCore
context.Entry(employee).Reload();
```


---

## 7. Key Methods Summary

| Purpose | Method |
|----------|--------|
| Query with SP | `DbSet.FromSqlInterpolated()` |
| Execute SP / Command | `Database.ExecuteSqlInterpolatedAsync()` |
| Get raw connection | `Database.GetDbConnection()` |
| Run migrations | `Database.Migrate()` |
| Reload entity | `context.Entry(entity).Reload()` |

---

## 8. Recommended Project Structure

// Models
``` EFCore
public class Employee { ... }
public class Department { ... }
```

// DbContext
``` EFCore
public class AppDbContext : DbContext { ... }
```

// Usage
``` EFCore
using var context = new AppDbContext();
```

// Prefer LINQ for standard operations
``` EFCore
var emp = await context.Employees.FindAsync(1);
```

// Use raw SQL or SP only when needed
``` EFCore
await context.Database.ExecuteSqlInterpolatedAsync($"EXEC RefreshData");
```


---

### ✅ Golden Rule
> Use **EF Core LINQ (IQueryable)** for 90% of your operations.  
> Use **ExecuteSqlInterpolatedAsync** only for:
> - Stored Procedures  
> - Bulk operations  
> - Legacy SQL integration  


----

### USER
for all the linq concepts category wise you can provide me the implementation in Query Syntax (SQL-like)  & method syntax as well for making the notes

### ASSISTANT
Absolutely! Below is a **category-wise breakdown of LINQ concepts** with **both Query Syntax (SQL-like)** and **Method Syntax** examples in C#. This is ideal for creating structured notes—especially helpful for .NET developers preparing for coding interviews or improving their LINQ fluency.

---

## 📌 1. **Filtering Operations**

### 📌 `Where` – Filters elements based on a condition

**Query Syntax:**
```csharp
var result = from num in numbers
             where num > 5
             select num;
```

**Method Syntax:**
```csharp
var result = numbers.Where(num => num > 5);
```

---

## 📌 2. **Projection Operations**

### 📌 `Select` – Transforms each element

**Query Syntax:**
```csharp
var squares = from num in numbers
              select num * num;
```

**Method Syntax:**
```csharp
var squares = numbers.Select(num => num * num);
```

### 📌 `SelectMany` – Flattens nested collections

**Query Syntax:**
```csharp
var allOrders = from customer in customers
                from order in customer.Orders
                select order;
```

**Method Syntax:**
```csharp
var allOrders = customers.SelectMany(customer => customer.Orders);
```

---

## 📌 3. **Sorting Operations**

### 📌 `OrderBy`, `OrderByDescending`, `ThenBy`

**Query Syntax:**
```csharp
var sorted = from p in products
             orderby p.Price descending, p.Name
             select p;
```

**Method Syntax:**
```csharp
var sorted = products
    .OrderByDescending(p => p.Price)
    .ThenBy(p => p.Name);
```

---

## 📌 4. **Grouping Operations**

### 📌 `GroupBy` – Groups elements by a key

**Query Syntax:**
```csharp
var groups = from p in products
             group p by p.Category into g
             select new { Category = g.Key, Count = g.Count() };
```

**Method Syntax:**
```csharp
var groups = products
    .GroupBy(p => p.Category)
    .Select(g => new { Category = g.Key, Count = g.Count() });
```

---

## 📌 5. **Set Operations**

### 📌 `Distinct` – Removes duplicates

**Query Syntax:** *(Not directly supported – use method syntax)*  
**Method Syntax:**
```csharp
var unique = numbers.Distinct();
```

### 📌 `Union`, `Intersect`, `Except`

**Method Syntax only:**
```csharp
var union = list1.Union(list2);
var intersect = list1.Intersect(list2);
var except = list1.Except(list2);
```

---

## 📌 6. **Join Operations**

### 📌 `Join` – Inner join

**Query Syntax:**
```csharp
var joined = from c in customers
             join o in orders on c.Id equals o.CustomerId
             select new { c.Name, o.Total };
```

**Method Syntax:**
```csharp
var joined = customers.Join(
    orders,
    c => c.Id,
    o => o.CustomerId,
    (c, o) => new { c.Name, o.Total });
```

### 📌 `GroupJoin` – Left outer join (with grouping)

**Query Syntax:**
```csharp
var groupJoin = from c in customers
                join o in orders on c.Id equals o.CustomerId into g
                select new { c.Name, Orders = g };
```

**Method Syntax:**
```csharp
var groupJoin = customers.GroupJoin(
    orders,
    c => c.Id,
    o => o.CustomerId,
    (c, g) => new { c.Name, Orders = g });
```

> **Left Outer Join (flattened):**
```csharp
var leftJoin = from c in customers
               join o in orders on c.Id equals o.CustomerId into g
               from order in g.DefaultIfEmpty()
               select new { c.Name, Total = order?.Total ?? 0 };
```

---

## 📌 7. **Aggregation Operations**

### 📌 `Count`, `Sum`, `Average`, `Min`, `Max`

**Query Syntax:** *(Not directly supported – use method syntax)*  
**Method Syntax:**
```csharp
int count = products.Count();
double total = products.Sum(p => p.Price);
double avg = products.Average(p => p.Price);
decimal max = products.Max(p => p.Price);
```

> You can combine with `Where`:
```csharp
double expensiveAvg = products
    .Where(p => p.Price > 100)
    .Average(p => p.Price);
```

---

## 📌 8. **Element Operations**

### 📌 `First`, `FirstOrDefault`, `Single`, `SingleOrDefault`, `Last`, `ElementAt`

**Method Syntax only:**
```csharp
var first = numbers.First();
var firstEven = numbers.FirstOrDefault(n => n % 2 == 0);
var onlyOne = products.Single(p => p.Id == 1);
var fifth = numbers.ElementAt(4);
```

> ⚠️ `Single` throws if 0 or >1 match; `First` returns first match (even if multiple).

---

## 📌 9. **Quantifier Operations**

### 📌 `Any`, `All`, `Contains`

**Method Syntax only:**
```csharp
bool hasEven = numbers.Any(n => n % 2 == 0);
bool allPositive = numbers.All(n => n > 0);
bool hasFive = numbers.Contains(5);
```

---

## 📌 10. **Partitioning Operations**

### 📌 `Take`, `Skip`, `TakeWhile`, `SkipWhile`

**Method Syntax only:**
```csharp
var top3 = numbers.Take(3);
var skip5 = numbers.Skip(5);
var takeWhilePositive = numbers.TakeWhile(n => n > 0);
```

> Combine for pagination:
```csharp
var page2 = products.Skip(10).Take(10);
```

---

## 📌 11. **Concatenation**

### 📌 `Concat`

**Method Syntax only:**
```csharp
var combined = list1.Concat(list2);
```

---

## 📌 12. **Conversion Operations**

### 📌 `ToArray`, `ToList`, `ToDictionary`, `Cast`, `OfType`

**Method Syntax only:**
```csharp
var list = query.ToList();
var dict = products.ToDictionary(p => p.Id);
var strings = objects.OfType<string>();
```

---

## ✅ Tips for Your Notes:
- **Query Syntax** is great for readability in complex queries (especially joins & groupings).
- **Method Syntax** is more powerful (supports all LINQ methods) and composable.
- Most real-world code uses **method syntax**, but interviews may test both.

Would you like a downloadable Markdown or PDF version of this for your notes? Or perhaps categorized code snippets in a GitHub Gist format?

### USER
int count = products.Count();
double total = products.Sum(p => p.Price);
double avg = products.Average(p => p.Price);
decimal max = products.Max(p => p.Price); this  can u implement in method syntax

### ASSISTANT
The code you've shown **is already in method syntax**! 😊

Let me clarify:

```csharp
int count = products.Count();
double total = products.Sum(p => p.Price);
double avg = products.Average(p => p.Price);
decimal max = products.Max(p => p.Price);
```

✅ **This is 100% LINQ Method Syntax** (also called *fluent syntax*).

---

### Why it's Method Syntax:
- It uses **extension methods** (`Count`, `Sum`, `Average`, `Max`) called directly on the collection (`products`).
- It uses **lambda expressions** (`p => p.Price`) for projections.
- There’s **no `from...select`** clause — that would be query syntax.

---

### For Comparison: Query Syntax (Not Possible for Aggregates)

LINQ **query syntax does not support aggregate functions directly** like `Count()`, `Sum()`, etc. You *cannot* write:

```csharp
// ❌ INVALID – This does NOT work in C# LINQ query syntax
var total = from p in products
            sum p.Price;
```

So for **aggregation operations**, **method syntax is the only option**.

However, you *can* combine query syntax with method syntax if needed:

```csharp
// First filter with query syntax, then aggregate with method syntax
var avgExpensive = (from p in products
                    where p.Price > 100
                    select p).Average(p => p.Price);
```

But the aggregation part (`Average`) is still method syntax.

---

### Summary:
| Operation      | Method Syntax ✅ | Query Syntax ❌ |
|----------------|------------------|-----------------|
| `Count()`      | ✔️               | Not supported   |
| `Sum()`        | ✔️               | Not supported   |
| `Average()`    | ✔️               | Not supported   |
| `Min()`/`Max()`| ✔️               | Not supported   |

So your original code is already the correct and standard **method syntax implementation** for aggregations. Keep using it as-is! 🚀

Let me know if you'd like to see how to combine filtering (`Where`) with these aggregates in both styles!

----
> ## DATA ANNOTATIONS

---

## 🔹 What is a Data Annotation?

A **Data Annotation** is an **attribute** you apply to a **property** (or class) to tell the framework something about it—like:

- Is it required?
- What’s the maximum length?
- Should it be treated as an email?
- How should it be displayed in a UI?

These are **attributes** in C#, which are a way to **attach metadata** to code elements like classes, methods, or properties.

> 💡 **Attribute (technical term)**: A special class that inherits from `System.Attribute`. It lets you add extra information (metadata) to your code, which other parts of the system (like EF Core or ASP.NET) can read and act upon.

Example of built-in data annotations:
```csharp
public class User
{
    [Required]               // ← This is a data annotation (attribute)
    [StringLength(100)]
    public string Name { get; set; }

    [EmailAddress]
    public string Email { get; set; }
}
```

These are part of the `System.ComponentModel.DataAnnotations` namespace.

---

## 🔹 How Are Built-in Annotations Like `[Required]` Implemented?

They are just **C# classes** that inherit from `ValidationAttribute` (for validation) or `Attribute` (for general metadata).

For example, the `[Required]` attribute is defined (simplified) like this inside .NET:

```csharp
public sealed class RequiredAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value != null && !string.IsNullOrWhiteSpace(value.ToString());
    }
}
```

> 💡 **ValidationAttribute**: A base class provided by .NET that you inherit from when creating **custom validation logic**. It has a method `IsValid` that you override to define your rule.

---

## 🔹 How to Create a **Custom Data Annotation**?

Let’s say you want to ensure a property is a **valid Indian mobile number** (10 digits, starts with 6–9). You can create your own attribute!

### ✅ Step 1: Create the Custom Attribute Class

```csharp
using System.ComponentModel.DataAnnotations;

public class IndianMobileAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // If value is null or empty, let [Required] handle it (optional)
        if (value == null)
            return ValidationResult.Success;

        string mobile = value.ToString() ?? "";

        // Check: 10 digits, starts with 6,7,8, or 9
        if (System.Text.RegularExpressions.Regex.IsMatch(mobile, @"^[6-9]\d{9}$"))
        {
            return ValidationResult.Success;
        }

        // Return error message
        return new ValidationResult("Mobile number must be a valid 10-digit Indian number starting with 6-9.");
    }
}
```

> 💡 **ValidationResult**: Represents the result of validation. `Success` means valid; otherwise, you return an error message.

> 💡 **ValidationContext**: Gives info about the object being validated (like its type, service provider, etc.). Useful for advanced scenarios.

---

### ✅ Step 2: Use It on Your Model

```csharp
public class Customer
{
    public int Id { get; set; }

    [IndianMobile]
    public string? Mobile { get; set; }
}
```

---

### ✅ Step 3: Trigger Validation

Data annotations **don’t auto-validate** unless something triggers it. Common triggers:

1. **In ASP.NET Core MVC**: Model binding automatically validates on POST.
2. **In EF Core**: By default, EF Core **does NOT run validation**—it only uses attributes like `[Required]` or `[StringLength]` for **database schema generation**, not runtime validation.
   - So if you want validation in EF Core, you must **manually validate** before saving.

#### 🔸 Manual Validation Example (works anywhere):

```csharp
using System.ComponentModel.DataAnnotations;

var customer = new Customer { Mobile = "12345" };
var context = new ValidationContext(customer);
var results = new List<ValidationResult>();

bool isValid = Validator.TryValidateObject(customer, context, results, validateAllProperties: true);

if (!isValid)
{
    foreach (var error in results)
    {
        Console.WriteLine(error.ErrorMessage);
    }
}
```

> 💡 **Validator.TryValidateObject**: A built-in .NET method that checks all data annotation rules on an object.

---

## 🔹 Key Points to Remember

| Concept | Explanation |
|--------|-------------|
| **Attribute** | A class that inherits from `System.Attribute`; used to attach metadata. |
| **Data Annotation** | A special kind of attribute used for validation or metadata (e.g., `[Required]`). |
| **ValidationAttribute** | Base class for custom validation logic. Override `IsValid`. |
| **EF Core & Validation** | EF Core **uses** data annotations for **database schema** (e.g., `NOT NULL`, `VARCHAR(100)`), but **does NOT validate** them at runtime. |
| **ASP.NET Core** | Automatically validates models on HTTP requests using these annotations. |

---

## 🔹 Bonus: Non-Validation Attributes (Just Metadata)

Not all data annotations are for validation. Some just describe **how data should be treated**.

Example: `[Column("user_email")]` in EF Core tells EF to map the property to a specific database column name.

```csharp
[Column("user_email")]
public string Email { get; set; }
```

This attribute comes from EF Core (`Microsoft.EntityFrameworkCore`), not `DataAnnotations`, but it’s still an **attribute**—just used for **configuration**, not validation.

---

## ✅ Summary

1. **Built-in data annotations** like `[Required]` are pre-made attributes for common rules.
2. To make your **own**, create a class that inherits from `ValidationAttribute`.
3. Override `IsValid` to define your logic.
4. Apply it to properties like `[YourCustomAttribute]`.
5. Remember: **EF Core doesn’t auto-validate**—you must trigger validation manually or rely on ASP.NET Core’s model binding.

---

## Data Annotations

``` C# code

1. MaxLength
2. StringLength
3. DisplayName
4. Column(DataType.Date)
5. Range
6. key
7. Required
8. DataType

```

-----
## IEnumerator,IEnumerable,ICollection,IQueryble,IList,List Concepts

---

### 1. **IEnumerable**
**What it is**:  
An interface that allows you to **enumerate** (i.e., loop through) a collection **one item at a time**, using a `foreach` loop.

**Key Points**:
- Read-only (you can’t add/remove items).
- Supports **deferred execution** (items are fetched one by one as you iterate).
- Implemented by almost all collections in .NET.

**When to use**:
- When you only need to **read** data sequentially.
- Great for returning data from methods without exposing the underlying collection type.

**Example**:
```csharp
public IEnumerable<int> GetNumbers()
{
    yield return 1;
    yield return 2;
    yield return 3;
}

// Usage
foreach (int num in GetNumbers())
{
    Console.WriteLine(num); // Prints 1, 2, 3
}
```

---

### 2. **IEnumerator**
**What it is**:  
The "engine" behind `IEnumerable`. It provides the actual mechanism to move through a collection (`MoveNext()`, `Current`, `Reset()`).

**Key Points**:
- You rarely implement this directly—usually via `yield return` or by implementing `IEnumerable`.
- `foreach` uses `IEnumerator` under the hood.

**When to use**:
- Only when you need **custom iteration logic** (e.g., traversing a tree or skipping items conditionally).

**Example**:
```csharp
// Usually hidden behind IEnumerable
var numbers = new List<int> { 1, 2, 3 };
var enumerator = numbers.GetEnumerator();
while (enumerator.MoveNext())
{
    Console.WriteLine(enumerator.Current);
}
enumerator.Dispose(); // Important!
```

---

### 3. **ICollection**
**What it is**:  
Extends `IEnumerable` and adds **size, add, remove, and clear** capabilities.

**Key Points**:
- Has a `.Count` property.
- Supports modifying the collection (`Add`, `Remove`, `Clear`).
- Still **not index-based** (no `collection[0]`).

**When to use**:
- When you need to know the **number of items** and modify the collection, but don’t need indexing.

**Example**:
```csharp
ICollection<string> names = new List<string>();
names.Add("Alice");
names.Add("Bob");
Console.WriteLine(names.Count); // 2
```

> 💡 `List<T>` implements `ICollection<T>`, so you can assign a `List` to an `ICollection` variable.

---

### 4. **IList**
**What it is**:  
Extends `ICollection` and adds **index-based access** (like an array).

**Key Points**:
- Supports `list[0]`, `Insert(index, item)`, etc.
- Can be **read/write by index**.

**When to use**:
- When you need to **access or modify items by position** (e.g., reorder, insert at specific index).

**Example**:
```csharp
IList<string> colors = new List<string> { "Red", "Green" };
colors.Add("Blue");
Console.WriteLine(colors[1]); // "Green"
colors[0] = "Yellow"; // Modify by index
```

---

### 5. **List<T>**
**What it is**:  
A **concrete class** that implements `IList<T>`, `ICollection<T>`, and `IEnumerable<T>`.

**Key Points**:
- Dynamic array (resizes automatically).
- Fast indexed access (`O(1)`).
- Slower for frequent insertions/deletions in the middle (`O(n)`).

**When to use**:
- Most common go-to collection when you need a **flexible, resizable list** with indexing.

**Example**:
```csharp
List<int> scores = new List<int> { 90, 85, 95 };
scores.Add(88);
int first = scores[0];
scores.RemoveAt(1);
```

---

### 6. **IQueryable**
**What it is**:  
Used for **querying data sources** (like databases) that support **LINQ**. It builds an **expression tree** instead of executing immediately.

**Key Points**:
- Deferred execution (like `IEnumerable`), but **queries are translated** (e.g., to SQL).
- Used with **Entity Framework**, **LINQ to SQL**, etc.
- `Where`, `Select`, etc., build expressions—not executed until `.ToList()` or enumeration.

**When to use**:
- When querying **external data sources** (databases, APIs with query support).
- To allow **composable queries** that run efficiently on the server.

**Example**:
```csharp
// Assume 'dbContext.Students' is DbSet<Student> → IQueryable<Student>
IQueryable<Student> teens = dbContext.Students
    .Where(s => s.Age >= 13 && s.Age <= 19);

// No query sent to DB yet!
var result = teens.ToList(); // Now the SQL query executes
```

> 🔍 **IEnumerable vs IQueryable**:  
> - `IEnumerable`: Runs in **memory** (client-side).  
> - `IQueryable`: Runs on the **data source** (e.g., database), more efficient for filtering large datasets.

---

### Summary Table

| Interface/Class   | Can Loop? | Know Count? | Add/Remove? | Index Access? | Deferred Execution? | Best For |
|-------------------|----------|-------------|-------------|----------------|----------------------|--------|
| `IEnumerable`     | ✅       | ❌          | ❌          | ❌             | ✅ (lazy)            | Read-only iteration |
| `IEnumerator`     | (internal use) | — | — | — | — | Custom iteration logic |
| `ICollection`     | ✅       | ✅          | ✅          | ❌             | ❌                   | Modify + count |
| `IList`           | ✅       | ✅          | ✅          | ✅             | ❌                   | Index-based access |
| `List<T>`         | ✅       | ✅          | ✅          | ✅             | ❌                   | General-purpose list |
| `IQueryable`      | ✅       | ❌*         | ❌          | ❌             | ✅ (with expression tree) | Database/API queries |

> *You can get count via `.Count()`, but it may trigger execution.

---

### Practical Advice: Which One to Use?

- **Return `IEnumerable<T>`** from methods if you only need to iterate (keeps your API flexible).
- **Use `List<T>`** when you’re working locally in memory and need full control.
- **Use `IQueryable<T>`** when working with Entity Framework or other queryable data sources.
- **Prefer interfaces (`IList`, `ICollection`) over concrete types** in method parameters for flexibility.

---



## Drop down implemenation in ASP.Net Core

### 1. Define the Models

```csharp
// Models/Product.cs
public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Cost { get; set; }
}
```

Create a **ViewModel** to hold both the selected product ID and the list of products:

```csharp
// Models/ProductViewModel.cs
public class ProductViewModel
{
    public int SelectedProductId { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
}
```

---

### 2. Controller Logic

In your controller (e.g., `HomeController`), populate the list and pass it to the view:

```csharp
// Controllers/HomeController.cs
public class HomeController : Controller
{
    public IActionResult Index()
    {
        var products = new List<Product>
        {
            new Product { Id = 1, ProductName = "Laptop", Cost = 999.99m },
            new Product { Id = 2, ProductName = "Mouse", Cost = 19.99m },
            new Product { Id = 3, ProductName = "Keyboard", Cost = 49.99m }
        };

        var viewModel = new ProductViewModel
        {
            Products = products
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Index(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            // model.SelectedProductId contains the selected product ID
            // You can now use it for further processing
            ViewBag.Message = $"You selected product ID: {model.SelectedProductId}";
        }

        // Repopulate the dropdown on postback (important!)
        model.Products = GetProducts(); // or reuse logic from GET
        return View(model);
    }

    private List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product { Id = 1, ProductName = "Laptop", Cost = 999.99m },
            new Product { Id = 2, ProductName = "Mouse", Cost = 19.99m },
            new Product { Id = 3, ProductName = "Keyboard", Cost = 49.99m }
        };
    }
}
```

> 💡 In real apps, you’d fetch products from a database or service.

---

### 3. View (Razor Page)

In your `Views/Home/Index.cshtml`:

```html
@model ProductViewModel

<h2>Select a Product</h2>

<form asp-action="Index" method="post">
    <div class="form-group">
        <label for="SelectedProductId">Choose a product:</label>
        <select asp-for="SelectedProductId" asp-items="@(new SelectList(Model.Products, "Id", "ProductName"))" class="form-control">
            <option value="">-- Select Product --</option>
        </select>
        <span asp-validation-for="SelectedProductId" class="text-danger"></span>
    </div>
    <button type="submit" class="btn btn-primary">Submit</button>
</form>

@if (ViewBag.Message != null)
{
    <div class="alert alert-info mt-3">@ViewBag.Message</div>
}
```



## Drop down implementation in ASP.Net MVC5(old scenario)

### 🖼️  View (`Views/Home/Index.cshtml`)

```html
@model ProductViewModel

@{
    ViewBag.Title = "Select Product";
}

<h2>Select a Product</h2>

@using (Html.BeginForm())
{
    <div class="form-group">
        @Html.LabelFor(m => m.SelectedProductId, "Choose a product:")
        @Html.DropDownListFor(
            m => m.SelectedProductId,
            new SelectList(Model.Products, "Id", "ProductName"),
            "-- Select Product --",
            new { @class = "form-control" }
        )
        @Html.ValidationMessageFor(m => m.SelectedProductId, "", new { @class = "text-danger" })
    </div>

    <input type="submit" value="Submit" class="btn btn-primary" />
}

@if (ViewBag.Message != null)
{
    <div class="alert alert-info">@ViewBag.Message</div>
}
```

---

## Syntax of the SelectList
### 📌 How `SelectList` Works

The `SelectList` constructor is:
```csharp
new SelectList(IEnumerable items, string dataValueField, string dataTextField)
```
- `dataValueField` → becomes the **`value` attribute** in `<option>`
- `dataTextField` → becomes the **visible text** inside `<option>`

So:
```csharp
new SelectList(Model.Products, "Id", "ProductName")
```
→ generates:
```html
<option value="1">Laptop</option>
<option value="2">Mouse</option>
```

---

## 📝 ASP.NET Core MVC – Tag Helper Syntax Cheat Sheet

### 1. **Anchor Tag Helper** (`<a>`)
Generates URLs using routing.

```html
<a asp-controller="Home" 
   asp-action="Index" 
   asp-route-id="123" 
   asp-route-category="books">Go to Home</a>
```
> Renders: `<a href="/Home/Index/123?category=books">Go to Home</a>`  
> (Exact URL depends on your route template)

---

### 2. **Form Tag Helper** (`<form>`)
Posts to a specific action.

```html
<form asp-controller="Account" 
      asp-action="Login" 
      method="post">
    <!-- Inputs here -->
    <button type="submit">Submit</button>
</form>
```
> Automatically includes anti-forgery token for `POST`.

---

### 3. **Input Tag Helper** (`<input>`)
Binds to model properties.

```html
<input asp-for="Email" class="form-control" />
<input asp-for="Password" type="password" class="form-control" />
<input asp-for="IsSubscribed" type="checkbox" />
```
> Generates correct `name`, `id`, `value`, and `type` based on model and data annotations.

---

### 4. **Label Tag Helper** (`<label>`)
Creates accessible labels.

```html
<label asp-for="Email"></label>
```
> Renders: `<label for="Email">Email</label>`  
> Display name uses `[Display(Name = "...")]` if present.

---

### 5. **Validation Message Tag Helper** (`<span>`)
Shows model validation errors.

```html
<span asp-validation-for="Email" class="text-danger"></span>
```
> Displays client-side & server-side error messages.

---

### 6. **Validation Summary Tag Helper**
Shows all validation errors at once.

```html
<div asp-validation-summary="All" class="text-danger"></div>
```
> Options: `All`, `ModelOnly`, `None`

---

### 7. **Select Tag Helper** (`<select>`)
Dropdown bound to model.

```html
<select asp-for="Country" asp-items="Model.CountryList"></select>
```
> `CountryList` is `IEnumerable<SelectListItem>` in your model.

**With default option:**
```html
<select asp-for="Country" asp-items="Model.CountryList">
    <option value="">-- Choose --</option>
</select>
```

---

### 8. **TextArea Tag Helper**
Multi-line text input.

```html
<textarea asp-for="Message" rows="4" class="form-control"></textarea>
```

---

### 9. **Environment Tag Helper**
Render content based on environment.

```html
<environment include="Development">
    <script src="~/js/site.js"></script>
</environment>
<environment exclude="Development">
    <script src="~/js/site.min.js"></script>
</environment>
```

---

### 10. **Partial Tag Helper**
Render a partial view.

```html
<partial name="_LoginPartial" />
<partial name="_ProductCard" for="SelectedProduct" />
```

---

### 11. **Cache Tag Helper**
Cache parts of a view.

```html
<cache expires-after="@TimeSpan.FromMinutes(10)">
    <p>Last server time: @DateTime.Now</p>
</cache>
```

---

### 12. **Script & Link Tag Helpers** (for bundling)
Used with `asp-src-include` / `asp-href-include` (less common in modern apps with bundlers).

```html
<script asp-src-include="~/lib/jquery/jquery.min.js"></script>
<link asp-href-include="~/css/site.css" rel="stylesheet" />
```

> ⚠️ Mostly useful if **not** using Webpack, Vite, or similar.

---

## 🔧 Required Setup

In your **`Views/_ViewImports.cshtml`**:
```csharp
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

> This enables all built-in MVC Tag Helpers globally.

---

## ✅ Pro Tips

- Always use **strongly-typed views**: `@model MyViewModel`
- Use `asp-for` **only with real model properties** (case-sensitive).
- `asp-route-*` → for **route parameters** (e.g., `asp-route-id`, `asp-route-slug`).
- Never mix `asp-for` and manual `name` — let Tag Helpers manage `name`.

---

You can save this as a Markdown or text file for quick reference during development! Let me know if you'd like a printable PDF version or examples with nested models (`asp-for="User.Address.City"`).
