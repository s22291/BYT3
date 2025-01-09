/** \file     Program.cs
*  \author    Aslan,Julio,Mohammad,Wiktor
*  \version   Final
*  \date      2024
*  \bug       No bugs so far
*  \copyright Polish and Japanies Information Technology 
*/

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;


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

        List<string> lisr = new List<string> { "d", "s", "s" };
        List<decimal> por = new List<decimal> { 2, 2, 4 };

        Ingredient ingredient = new Ingredient("sxs", lisr, por, DateTime.Now);

        ingredient.CountryOfOrigin.Add("Poland");
    }
}

