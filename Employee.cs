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
    // Basic attributes
    public string Name { get; set; }        // Employee's first name
    public string Surname { get; set; }     // Employee's last name
    public decimal Salary { get; set; }     // Employee's monthly salary (decimal for monetary values)
    public int Experience { get; set; }     // Employee's years of experience

    public Employee()
    {
        // Optional: Initialize default values for properties
        Name = "Unknown";
        Surname = "Unknown";
        Salary = 0m;
        Experience = 0;
    }
    // Optional attribute
    public DateTime? WorkedOvertimeDate { get; set; } // Nullable to handle 0..1 cardinality

    // MultiValue attribute
    public List<string> Skills { get; set; } = new List<string>(); // List of employee's skills

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
        Name = name;
        Surname = surname;
        Salary = salary;
        Experience = experience;
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

    // Method to display employee information
    public void DisplayEmployeeInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Surname: {Surname}");
        Console.WriteLine($"Salary: {Salary:C}");
        Console.WriteLine($"Annual Salary: {AnnualSalary:C}"); // Derived attribute
        Console.WriteLine($"Experience: {Experience} years");
        Console.WriteLine($"Worked Overtime Date: {WorkedOvertimeDate?.ToString("d") ?? "None"}");
        Console.WriteLine("Skills: " + (Skills.Count > 0 ? string.Join(", ", Skills) : "None"));
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
