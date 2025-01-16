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
using System.IO;


class Program
{
    static void Main(string[] args) // Entry point of the program
    {

     //For Employee Objects
        string filePath = "EmployeesData.xml";
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found. Creating default data...");

            var employee1 = new Employee("John", "Doe", 50000, 5);
            var employee2 = new Employee("Jane", "Smith", 60000, 8, new DateTime(2023, 1, 15));
         
    
            Employee.SaveToFile(filePath);
        }
            Employee.LoadFromFile(filePath);
            //Test for empty variables
            // var employee3 = new Employee("Juan", " ", 50000, 5);
            Employee.DisplayAllEmployees();
            Employee.SaveToFile(filePath);
          


        //For Cooks Objects
        // string filePath = "CooksData.xml";
        // if (!File.Exists(filePath))
        // {
        //     Console.WriteLine("File not found. Creating default data...");

        //     var cook1 = new Cook(Cook.RoleType.ExecutiveChef, true, "Beef Wellington");
        //     var cook2 = new Cook(Cook.RoleType.SousChef, false, " ");
    
        //      Cook.SaveToFile(filePath);
        // }
        //     Cook.LoadFromFile(filePath);
        //     Cook.DisplayAllCooks();



            //Ingredient object Protection for more than 3 ingredients of the same country
            // var ingredient1 = new Ingredient("Spice", "Egypt", 2.5m);
            // var ingredient2 = new Ingredient("Herb", "Egypt", 1.2m);
            // var ingredient3 = new Ingredient("Vegetable", "Egypt", 3.0m);
            // var ingredient4 = new Ingredient("Fruit", "Egypt", 1.5m);

            // Ingredient.DisplayAllIngredients();

    }
}

