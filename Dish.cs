/** \file     Dish.cs
*  \author    Aslan,Julio,Mohammad,Wiktor
*  \version   Final
*  \date      2024
*  \bug       No bugs so far
*  \copyright Polish and Japanies Information Technology 
*/

using System;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class Dish
{
    // Basic Attribute
    public string CourseType { get; set; } // The course type (e.g., "Starter", "Main", "Dessert")

    // Optional Attribute
    public string SpecialDiet { get; set; } // Optional special diet (e.g., "Vegan", "Gluten-Free")

    // MultiValue Attribute
    public List<string> Ingredients { get; set; } = new List<string>(); // List of ingredients used in the dish

    // Static Attribute
    public static int TotalDishes { get; private set; } = 0; // Total number of Dish instances created

    // Class Extent
    private static List<Dish> Dishes = new List<Dish>(); // Stores all Dish instances

    // Complex Attribute
    public DateTime DateAddedToMenu { get; set; } // The date the dish was added to the menu

    // Basic Attribute
    public string ServiceCutleryType { get; set; } // The type of service cutlery (e.g., "Fork", "Spoon", "Knife")

    // Derived Attribute
    public bool IsNew
    {
        get
        {
            // If the dish was added to the menu in the last 30 days, it is considered "new"
            return (DateTime.Now - DateAddedToMenu).TotalDays <= 30;
        }
    }

    // Constructor
    public Dish(string courseType, string serviceCutleryType, DateTime dateAddedToMenu, string? specialDiet = null)
    {
        CourseType = courseType;
        ServiceCutleryType = serviceCutleryType;
        DateAddedToMenu = dateAddedToMenu;
        SpecialDiet = specialDiet; // Optional attribute

        // Increment static count and add to class extent
        TotalDishes++;
        Dishes.Add(this);
    }

    // Method to display dish information
    public void DisplayDishInfo()
    {
        Console.WriteLine($"Course Type: {CourseType}");
        Console.WriteLine($"Service Cutlery Type: {ServiceCutleryType}");
        Console.WriteLine($"Date Added to Menu: {DateAddedToMenu:yyyy-MM-dd}");
        Console.WriteLine($"Special Diet: {SpecialDiet ?? "None"}"); // Handles optional attribute
        Console.WriteLine($"Is New: {(IsNew ? "Yes" : "No")}");
        Console.WriteLine("Ingredients: " + (Ingredients.Count > 0 ? string.Join(", ", Ingredients) : "None"));
    }

    // Static Method to Display All Dishes
    public static void DisplayAllDishes()
    {
        Console.WriteLine("\n--- All Dishes ---");
        foreach (var dish in Dishes)
        {
            dish.DisplayDishInfo();
            Console.WriteLine();
        }
    }

    // Serialization Method
    public static void SaveToFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Dish>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Dishes);
            }
            Console.WriteLine("Dishes saved to file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    // Deserialization Method
    public static void LoadFromFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Dish>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Dishes = (List<Dish>?)serializer.Deserialize(reader) ?? new List<Dish>();
            }
            Console.WriteLine("Dishes loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}


