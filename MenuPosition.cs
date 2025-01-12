/** \file     MenuPosition.cs
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

public class MenuPosition
{
    // Basic Attribute
    public string Name { get; set; } // Name of the menu item

    // Optional Attribute
    public string Description { get; set; }
    public string Allergens { get; set; } // Optional information about allergens (e.g., "Contains nuts")

    // MultiValue Attribute
    public List<string> Ingredients { get; set; } = new List<string>(); // List of ingredients for the menu item

    // Static Attribute
    public static int TotalMenuPositions { get; private set; } = 0; // Total number of MenuPosition instances created

    // Class Extent
    private static List<MenuPosition> MenuPositions = new List<MenuPosition>(); // Stores all MenuPosition instances

    // Complex Attribute
    public DateTime DateAddedToMenu { get; set; } // Date when the menu item was added to the menu

    // Basic Attributes
    public decimal ProductionCost { get; set; } // Production cost of the menu item
    public decimal SellPrice { get; set; }      // Selling price of the menu item
    public string Temperature { get; set; }    // Temperature category (e.g., "Hot", "Cold")

    // Constant Desired Profit Margin
    public const decimal DesiredProfitMargin = 0.09m;

    // Derived Attribute
    public decimal ActualProfitMargin
    {
        get
        {
            if (SellPrice > 0)
            {
                return (SellPrice - ProductionCost) / SellPrice;
            }
            return 0; // Avoid division by zero
        }
    }

    // Constructor
    public MenuPosition(string name, decimal productionCost, decimal sellPrice, string temperature, DateTime dateAddedToMenu, string? allergens = null)
    {
        Name = name;
        ProductionCost = productionCost;
        SellPrice = sellPrice;
        Temperature = temperature;
        DateAddedToMenu = dateAddedToMenu;
        Allergens = allergens; // Optional attribute

        // Increment static count and add to class extent
        TotalMenuPositions++;
        MenuPositions.Add(this);
    }

    // Method to display menu position information
    public void DisplayMenuPositionInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Production Cost: {ProductionCost:C}");
        Console.WriteLine($"Sell Price: {SellPrice:C}");
        Console.WriteLine($"Temperature: {Temperature}");
        Console.WriteLine($"Date Added to Menu: {DateAddedToMenu:yyyy-MM-dd}");
        Console.WriteLine($"Allergens: {Allergens ?? "None"}");
        Console.WriteLine($"Desired Profit Margin: {DesiredProfitMargin:P}");
        Console.WriteLine($"Actual Profit Margin: {ActualProfitMargin:P}");
        Console.WriteLine("Ingredients: " + (Ingredients.Count > 0 ? string.Join(", ", Ingredients) : "None"));
    }

    // Static Method to Display All Menu Positions
    public static void DisplayAllMenuPositions()
    {
        Console.WriteLine("\n--- All Menu Positions ---");
        foreach (var menuPosition in MenuPositions)
        {
            menuPosition.DisplayMenuPositionInfo();
            Console.WriteLine();
        }
    }

    // Serialization Method
    public static void SaveToFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<MenuPosition>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, MenuPositions);
            }
            Console.WriteLine("Menu positions saved to file successfully.");
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
            XmlSerializer serializer = new XmlSerializer(typeof(List<MenuPosition>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                MenuPositions = (List<MenuPosition>?)serializer.Deserialize(reader) ?? new List<MenuPosition>();
            }
            Console.WriteLine("Menu positions loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}


