/** \file     Program.cs
*  \author    Aslan,Julio,Mohammad,Wiktor
*  \version   Final
*  \date      2024
*  \bug       No bugs so far
*  \copyright Polish and Japanies Information Technology 
*/

using System;


class Program
{
    static void Main(string[] args) // Entry point of the program
    {
        Employee employee = new Employee();
        employee.Name = "Olga";
        employee.Surname = "Smith";
        employee.Salary = 5000m;
        employee.Experience = 3;
        employee.WorkedOvertimeDate = new DateTime(2021, 5, 15);
        employee.Skills.Add("C#");
        employee.Skills.Add("SQL");

        // Print employee information
        Console.WriteLine(employee.Name);
        Console.WriteLine(employee.Surname);
        Console.WriteLine(employee.Salary);
        Console.WriteLine(employee.Experience);
        Console.WriteLine(employee.WorkedOvertimeDate);
        Console.WriteLine(employee.Skills.Count);
        Console.WriteLine(employee.Skills[0]);
        Console.WriteLine(employee.Skills[1]);

    }
}

