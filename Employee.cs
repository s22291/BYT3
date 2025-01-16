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
    // -----------------------------------------------------
    // Private backing fields
    // -----------------------------------------------------
    private string name;
    private string surname;
    private decimal salary;
    private int experience;

    // -----------------------------------------------------
    // Original properties (private setters).
    // We mark them [XmlIgnore] so the serializer DOES NOT
    // try to use these private setters directly.
    // -----------------------------------------------------

    [XmlIgnore]
    public string Name
    {
        get { return name; }
        private set { name = ValidateNonEmptyString(value, nameof(Name)); }
    }

    [XmlIgnore]
    public string Surname
    {
        get { return surname; }
        private set { surname = ValidateNonEmptyString(value, nameof(Surname)); }
    }

    [XmlIgnore]
    public decimal Salary
    {
        get { return salary; }
        private set
        {
            if (value <= 0)
                throw new ArgumentException("Salary must be greater than zero.");
            salary = value;
        }
    }

    [XmlIgnore]
    public int Experience
    {
        get { return experience; }
        private set
        {
            if (value < 0)
                throw new ArgumentException("Experience cannot be negative.");
            experience = value;
        }
    }

    // -----------------------------------------------------
    // Bridge properties for XML Serialization.
    // These are public so the XML serializer can set them.
    // They read/write the same private fields through
    // the original (private) setters — *within the same class*,
    // so it's allowed.
    // -----------------------------------------------------

    [XmlElement("Name")]
    public string NameForXml
    {
        get => name;
        set => Name = value; // Calls the private setter logic
    }

    [XmlElement("Surname")]
    public string SurnameForXml
    {
        get => surname;
        set => Surname = value; // Calls the private setter logic
    }

    [XmlElement("Salary")]
    public decimal SalaryForXml
    {
        get => salary;
        set => Salary = value; // Calls the private setter logic
    }

    [XmlElement("Experience")]
    public int ExperienceForXml
    {
        get => experience;
        set => Experience = value; // Calls the private setter logic
    }

    // -----------------------------------------------------
    // Other properties
    // -----------------------------------------------------

    // Optional Attribute (already public with public setter)
    public DateTime? WorkedOvertimeDate { get; set; }

    // Static attribute
    public static int TotalEmployees { get; private set; } = 0;

    // Class extent (stores all employee objects)
    private static List<Employee> Employees = new List<Employee>();

    // Derived attribute (no setter, so typically [XmlIgnore])
    [XmlIgnore]
    public decimal AnnualSalary
    {
        get { return Salary * 12; } // Calculate annual salary from monthly salary
    }

    // -----------------------------------------------------
    // Parameterless constructor (REQUIRED for XML serializer).
    // Provide safe defaults. DO NOT increment `TotalEmployees`
    // here, or you might double-count during deserialization.
    // -----------------------------------------------------
    public Employee()
    {
        name = "Unknown";
        surname = "Unknown";
        salary = 1m;
        experience = 0;
        WorkedOvertimeDate = null;
        // No increment of TotalEmployees here
    }

    // -----------------------------------------------------
    // Main Constructors
    // -----------------------------------------------------
    public Employee(string name, string surname, decimal salary, int experience)
    {
        Name = name;        // Validation
        Surname = surname;  // Validation
        Salary = salary;    // Validation
        Experience = experience;

        WorkedOvertimeDate = null;

        // Increment count and store in class extent
        TotalEmployees++;
        Employees.Add(this);
    }

    // Overload constructor to include overtime date
    public Employee(string name, string surname, decimal salary, int experience, DateTime workedOvertimeDate)
        : this(name, surname, salary, experience)
    {
        WorkedOvertimeDate = workedOvertimeDate;
    }

    // -----------------------------------------------------
    // Methods to update specific fields
    // -----------------------------------------------------
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
            throw new ArgumentException("Salary must be greater than zero.");
        Salary = newSalary;
    }

    public void UpdateExperience(int newExperience)
    {
        if (newExperience < 0)
            throw new ArgumentException("Experience cannot be negative.");
        Experience = newExperience;
    }

    // -----------------------------------------------------
    // Private static helper method for validation
    // -----------------------------------------------------
    private static string ValidateNonEmptyString(string value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{propertyName} cannot be null, empty, or whitespace.");
        }
        return value;
    }

    // -----------------------------------------------------
    // Method to display employee information
    // -----------------------------------------------------
    public void DisplayEmployeeInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Surname: {Surname}");
        Console.WriteLine($"Salary: {Salary:C}");
        Console.WriteLine($"Annual Salary: {AnnualSalary:C}");
        Console.WriteLine($"Experience: {Experience} years");
        Console.WriteLine($"Worked Overtime Date: {WorkedOvertimeDate?.ToString("d") ?? "None"}");
    }

    // -----------------------------------------------------
    // Static method to display all employees
    // -----------------------------------------------------
    public static void DisplayAllEmployees()
    {
        Console.WriteLine("\n--- All Employees ---");
        foreach (var employee in Employees)
        {
            employee.DisplayEmployeeInfo();
            Console.WriteLine();
        }
    }

    // -----------------------------------------------------
    // Serialization / Deserialization
    // -----------------------------------------------------
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


