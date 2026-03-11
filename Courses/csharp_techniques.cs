using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpTechniques
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("=== C# Essential Techniques ===\n");

            // Sample data
            var list1 = new List<int> { 1, 2, 3, 4, 5 };
            var list2 = new List<int> { 4, 5, 6, 7, 8 };
            
            var people = new List<Person>
            {
                new Person { Id = 1, Name = "Alice", Age = 25, City = "New York" },
                new Person { Id = 2, Name = "Bob", Age = 30, City = "London" },
                new Person { Id = 3, Name = "Charlie", Age = 25, City = "New York" },
                new Person { Id = 4, Name = "David", Age = 35, City = "Paris" }
            };

            // ============================================
            // 1. LINQ - Language Integrated Query
            // ============================================
            Console.WriteLine("1. LINQ Queries:");
            
            // Where - Filter data
            var adults = people.Where(p => p.Age >= 30);
            Console.WriteLine("Adults (Age >= 30): " + string.Join(", ", adults.Select(p => p.Name)));
            
            // Select - Transform data
            var names = people.Select(p => p.Name);
            Console.WriteLine("Names only: " + string.Join(", ", names));
            
            // OrderBy / OrderByDescending - Sort data
            var sortedByAge = people.OrderBy(p => p.Age).ThenBy(p => p.Name);
            Console.WriteLine("Sorted by age: " + string.Join(", ", sortedByAge.Select(p => $"{p.Name}({p.Age})")));

            // ============================================
            // 2. List Comparison Techniques
            // ============================================
            Console.WriteLine("\n2. List Comparison:");
            
            // Intersect - Common elements
            var common = list1.Intersect(list2);
            Console.WriteLine($"Common elements: {string.Join(", ", common)}");
            
            // Except - Elements in first but not in second
            var onlyInList1 = list1.Except(list2);
            Console.WriteLine($"Only in list1: {string.Join(", ", onlyInList1)}");
            
            // Union - All unique elements from both
            var allUnique = list1.Union(list2);
            Console.WriteLine($"Union: {string.Join(", ", allUnique)}");
            
            // SequenceEqual - Check if lists are identical
            var areEqual = list1.SequenceEqual(list2);
            Console.WriteLine($"Lists equal: {areEqual}");

            // ============================================
            // 3. Aggregation Methods
            // ============================================
            Console.WriteLine("\n3. Aggregation:");
            
            var numbers = new List<int> { 10, 20, 30, 40, 50 };
            Console.WriteLine($"Sum: {numbers.Sum()}");
            Console.WriteLine($"Average: {numbers.Average()}");
            Console.WriteLine($"Min: {numbers.Min()}");
            Console.WriteLine($"Max: {numbers.Max()}");
            Console.WriteLine($"Count: {numbers.Count()}");
            
            // Any - Check if any element matches condition
            var hasTeens = people.Any(p => p.Age < 20);
            Console.WriteLine($"Has teens: {hasTeens}");
            
            // All - Check if all elements match condition
            var allAdults = people.All(p => p.Age >= 18);
            Console.WriteLine($"All adults: {allAdults}");

            // ============================================
            // 4. GroupBy - Group data by property
            // ============================================
            Console.WriteLine("\n4. GroupBy:");
            
            var groupedByCity = people.GroupBy(p => p.City);
            foreach (var group in groupedByCity)
            {
                Console.WriteLine($"{group.Key}: {string.Join(", ", group.Select(p => p.Name))}");
            }
            
            var groupedByAge = people.GroupBy(p => p.Age);
            foreach (var group in groupedByAge)
            {
                Console.WriteLine($"Age {group.Key}: {group.Count()} people");
            }

            // ============================================
            // 5. First, FirstOrDefault, Single, SingleOrDefault
            // ============================================
            Console.WriteLine("\n5. Finding Elements:");
            
            var firstPerson = people.First();
            Console.WriteLine($"First person: {firstPerson.Name}");
            
            var firstInLondon = people.FirstOrDefault(p => p.City == "London");
            Console.WriteLine($"First in London: {firstInLondon?.Name ?? "None"}");
            
            // Single - throws exception if more than one or zero elements
            // Use when you expect exactly one match
            var thirtyYearOld = people.SingleOrDefault(p => p.Age == 30);
            Console.WriteLine($"Single 30-year-old: {thirtyYearOld?.Name ?? "None"}");

            // ============================================
            // 6. Distinct - Remove duplicates
            // ============================================
            Console.WriteLine("\n6. Distinct:");
            
            var ages = people.Select(p => p.Age).Distinct();
            Console.WriteLine($"Unique ages: {string.Join(", ", ages)}");
            
            var cities = people.Select(p => p.City).Distinct();
            Console.WriteLine($"Unique cities: {string.Join(", ", cities)}");

            // ============================================
            // 7. Skip and Take - Pagination
            // ============================================
            Console.WriteLine("\n7. Pagination:");
            
            var page1 = people.Skip(0).Take(2);
            Console.WriteLine($"Page 1: {string.Join(", ", page1.Select(p => p.Name))}");
            
            var page2 = people.Skip(2).Take(2);
            Console.WriteLine($"Page 2: {string.Join(", ", page2.Select(p => p.Name))}");

            // ============================================
            // 8. Join - Combine two lists
            // ============================================
            Console.WriteLine("\n8. Join:");
            
            var orders = new List<Order>
            {
                new Order { OrderId = 101, PersonId = 1, Product = "Laptop" },
                new Order { OrderId = 102, PersonId = 2, Product = "Phone" },
                new Order { OrderId = 103, PersonId = 1, Product = "Mouse" }
            };
            
            var personOrders = people.Join(
                orders,
                person => person.Id,
                order => order.PersonId,
                (person, order) => new { person.Name, order.Product }
            );
            
            foreach (var po in personOrders)
            {
                Console.WriteLine($"{po.Name} ordered {po.Product}");
            }

            // ============================================
            // 9. Null Conditional Operator (?.)
            // ============================================
            Console.WriteLine("\n9. Null Conditional:");
            
            Person nullPerson = null;
            Console.WriteLine($"Null person name: {nullPerson?.Name ?? "No name"}");
            
            var firstOrNull = people.FirstOrDefault(p => p.Age > 100);
            Console.WriteLine($"Person > 100 years: {firstOrNull?.Name ?? "Not found"}");

            // ============================================
            // 10. Pattern Matching (C# 7.0+)
            // ============================================
            Console.WriteLine("\n10. Pattern Matching:");
            
            foreach (var person in people.Take(2))
            {
                var description = person.Age switch
                {
                    < 18 => "Minor",
                    >= 18 and < 30 => "Young Adult",
                    >= 30 and < 60 => "Adult",
                    _ => "Senior"
                };
                Console.WriteLine($"{person.Name} is a {description}");
            }

            // ============================================
            // 11. Dictionary Techniques
            // ============================================
            Console.WriteLine("\n11. Dictionary:");
            
            var dict = people.ToDictionary(p => p.Id, p => p.Name);
            Console.WriteLine($"Person with ID 2: {dict[2]}");
            
            // TryGetValue - Safe way to get value
            if (dict.TryGetValue(5, out string name))
                Console.WriteLine($"Found: {name}");
            else
                Console.WriteLine("ID 5 not found");

            // ============================================
            // 12. String Manipulation with LINQ
            // ============================================
            Console.WriteLine("\n12. String Manipulation:");
            
            var sentence = "Hello World from C# Programming";
            var words = sentence.Split(' ');
            var longWords = words.Where(w => w.Length > 5);
            Console.WriteLine($"Long words: {string.Join(", ", longWords)}");

            // ============================================
            // 13. Conditional Expressions (Ternary)
            // ============================================
            Console.WriteLine("\n13. Ternary Operator:");
            
            var age = 20;
            var status = age >= 18 ? "Adult" : "Minor";
            Console.WriteLine($"Age {age} is {status}");

            // ============================================
            // 14. Zip - Combine two sequences
            // ============================================
            Console.WriteLine("\n14. Zip:");
            
            var listA = new List<string> { "A", "B", "C" };
            var listB = new List<int> { 1, 2, 3 };
            var zipped = listA.Zip(listB, (a, b) => $"{a}{b}");
            Console.WriteLine($"Zipped: {string.Join(", ", zipped)}");

            // ============================================
            // 15. SelectMany - Flatten nested collections
            // ============================================
            Console.WriteLine("\n15. SelectMany:");
            
            var departments = new List<Department>
            {
                new Department { Name = "IT", Employees = new List<string> { "John", "Jane" } },
                new Department { Name = "HR", Employees = new List<string> { "Bob", "Alice" } }
            };
            
            var allEmployees = departments.SelectMany(d => d.Employees);
            Console.WriteLine($"All employees: {string.Join(", ", allEmployees)}");
        }
    }

    // Helper classes
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int PersonId { get; set; }
        public string Product { get; set; }
    }

    public class Department
    {
        public string Name { get; set; }
        public List<string> Employees { get; set; }
    }
}