/** \file     Drink.cs
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

public class Drink
{
    // Basic Attribute
    public string DrinkwareType { get; set; } // Type of drinkware (e.g., "Glass", "Mug", "Wine Glass")

    // Basic Attribute
    public bool IfSparkling { get; set; } // Whether the drink is sparkling or not

    // Optional Attribute
    public string FlavorProfile { get; set; } // Optional attribute to specify the drink's flavor profile (e.g., "Citrus", "Sweet")

    // MultiValue Attribute
    public List<string> Ingredients { get; set; } = new List<string>(); // List of ingredients in the drink

    // Static Attribute
    public static int TotalDrinks { get; private set; } = 0; // Total number of Drink instances created

    // Class Extent
    private static List<Drink> Drinks = new List<Drink>(); // Stores all Drink instances

    // Complex Attribute
    public DateTime DateAddedToMenu { get; set; } // Date when the drink was added to the menu

    // Derived Attribute
    public bool IsAlcoholic
    {
        get
        {
            // Check if the list of ingredients contains alcohol-related terms
            return Ingredients.Exists(ingredient => ingredient.ToLower().Contains("alcohol") || ingredient.ToLower().Contains("vodka") || ingredient.ToLower().Contains("wine"));
        }
    }

    // Constructor
    public Drink(string drinkwareType, bool ifSparkling, DateTime dateAddedToMenu, string? flavorProfile = null)
    {
        DrinkwareType = drinkwareType;
        IfSparkling = ifSparkling;
        DateAddedToMenu = dateAddedToMenu;
        FlavorProfile = flavorProfile; // Optional attribute

        // Increment static count and add to class extent
        TotalDrinks++;
        Drinks.Add(this);
    }

    // Method to display drink information
    public void DisplayDrinkInfo()
    {
        Console.WriteLine($"Drinkware Type: {DrinkwareType}");
        Console.WriteLine($"Is Sparkling: {(IfSparkling ? "Yes" : "No")}");
        Console.WriteLine($"Date Added to Menu: {DateAddedToMenu:yyyy-MM-dd}");
        Console.WriteLine($"Flavor Profile: {FlavorProfile ?? "None"}");
        Console.WriteLine($"Is Alcoholic: {(IsAlcoholic ? "Yes" : "No")}");
        Console.WriteLine("Ingredients: " + (Ingredients.Count > 0 ? string.Join(", ", Ingredients) : "None"));
    }

    // Static Method to Display All Drinks
    public static void DisplayAllDrinks()
    {
        Console.WriteLine("\n--- All Drinks ---");
        foreach (var drink in Drinks)
        {
            drink.DisplayDrinkInfo();
            Console.WriteLine();
        }
    }

    // Serialization Method
    public static void SaveToFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Drink>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Drinks);
            }
            Console.WriteLine("Drinks saved to file successfully.");
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
            XmlSerializer serializer = new XmlSerializer(typeof(List<Drink>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Drinks = (List<Drink>?)serializer.Deserialize(reader) ?? new List<Drink>();
            }
            Console.WriteLine("Drinks loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}


