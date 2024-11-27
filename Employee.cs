public class Employee
{
    // Properties of the Employee class
    public string Name { get; set; }        // Employee's first name
    public string Surname { get; set; }     // Employee's last name
    public decimal Salary { get; set; }     // Employee's salary (decimal for monetary values)
    public int Experience { get; set; }     // Employee's years of experience
    
    // Optional property for overtime dates
    public DateTime? WorkedOvertimeDate { get; set; } // Nullable to handle 0..1 cardinality

    // Constructor for initializing Employee objects
    public Employee(string name, string surname, decimal salary, int experience)
    {
        Name = name;
        Surname = surname;
        Salary = salary;
        Experience = experience;
        WorkedOvertimeDate = null; // Default is null (no overtime worked yet)
    }

    // Overload constructor to include overtime date
    public Employee(string name, string surname, decimal salary, int experience, DateTime workedOvertimeDate)
    {
        Name = name;
        Surname = surname;
        Salary = salary;
        Experience = experience;
        WorkedOvertimeDate = workedOvertimeDate;
    }

    // Method to display employee information
    public void DisplayEmployeeInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Surname: {Surname}");
        Console.WriteLine($"Salary: {Salary:C}"); // Format salary as currency
        Console.WriteLine($"Experience: {Experience} years");
        Console.WriteLine($"Worked Overtime Date: {WorkedOvertimeDate?.ToString("d") ?? "None"}");
    }
}
