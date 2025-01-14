/** \file     Employee.cs
*  \author    Aslan,Julio,Mohammad,Wiktor
*  \version   Final
*  \date      2024
*  \bug       No bugs so far
*  \copyright Polish and Japanies Information Technology 
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;



public class Employee
{
    // Private backing fields for validation
    private string name;
    private string surname;
    private decimal salary;
    private int experience;

    // Public properties with validation
    public string Name
    {
        get { return name; }
        private set { name = ValidateNonEmptyString(value, nameof(Name)); }
    }

    public string Surname
    {
        get { return surname; }
        private set { surname = ValidateNonEmptyString(value, nameof(Surname)); }
    }

    public decimal Salary
    {
        get { return salary; }
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Salary must be greater than zero.");
            }
            salary = value;
        }
    }

    public int Experience
    {
        get { return experience; }
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Experience cannot be negative.");
            }
            experience = value;
        }
    }

    // Optional Attribute
    public DateTime? WorkedOvertimeDate { get; set; } // Nullable to handle 0..1 cardinality

    // Static attribute
    public static int TotalEmployees { get; private set; } = 0; // Total number of employees

    // Class extent (stores all employee objects)
    private static List<Employee> Employees = new List<Employee>();

    // Derived attribute
    public decimal AnnualSalary
    {
        get { return Salary * 12; } // Calculate annual salary from monthly salary
    }

    // Constructor for initializing Employee objects
    public Employee(string name, string surname, decimal salary, int experience)
    {
        Name = name;         // Validation will happen here
        Surname = surname;   // Validation will happen here
        Salary = salary;     // Validation will happen here
        Experience = experience; // Validation will happen here
        WorkedOvertimeDate = null; // Default is null (no overtime worked yet)

        // Increment static count and add to class extent
        TotalEmployees++;
        Employees.Add(this);
    }

    // Overload constructor to include overtime date
    public Employee(string name, string surname, decimal salary, int experience, DateTime workedOvertimeDate)
        : this(name, surname, salary, experience)
    {
        WorkedOvertimeDate = workedOvertimeDate;
    }

    // Methods to update specific fields
    public void UpdateName(string newName)
    {
        Name = ValidateNonEmptyString(newName, nameof(Name));
    }

    public void UpdateSurname(string newSurname)
    {
        Surname = ValidateNonEmptyString(newSurname, nameof(Surname));
    }

    public void UpdateSalary(decimal newSalary)
    {
        if (newSalary <= 0)
        {
            throw new ArgumentException("Salary must be greater than zero.");
        }
        Salary = newSalary;
    }

    public void UpdateExperience(int newExperience)
    {
        if (newExperience < 0)
        {
            throw new ArgumentException("Experience cannot be negative.");
        }
        Experience = newExperience;
    }

    // Private static helper method for validation
    private static string ValidateNonEmptyString(string value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{propertyName} cannot be null, empty, or whitespace.");
        }
        return value;
    }

    // Method to display employee information
    public void DisplayEmployeeInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Surname: {Surname}");
        Console.WriteLine($"Salary: {Salary:C}");
        Console.WriteLine($"Annual Salary: {AnnualSalary:C}"); // Derived attribute
        Console.WriteLine($"Experience: {Experience} years");
        Console.WriteLine($"Worked Overtime Date: {WorkedOvertimeDate?.ToString("d") ?? "None"}");
    }

    // Static method to display all employees
    public static void DisplayAllEmployees()
    {
        Console.WriteLine("\n--- All Employees ---");
        foreach (var employee in Employees)
        {
            employee.DisplayEmployeeInfo();
            Console.WriteLine();
        }
    }

    // Serialization method
    public static void SaveToFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Employees);
            }
            Console.WriteLine("Employees saved to file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    // Deserialization method
    public static void LoadFromFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Employees = (List<Employee>?)serializer.Deserialize(reader) ?? new List<Employee>();
            }
            Console.WriteLine("Employees loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}

