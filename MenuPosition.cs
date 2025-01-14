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
    // Private fields
    private string name; // Name of the menu item
    private decimal productionCost; // Production cost of the menu item
    private decimal sellPrice; // Selling price of the menu item
    private string temperature; // Temperature category (e.g., "Hot", "Cold")

    // Public read-only properties
    public string Name
    {
        get { return name; }
    }

    public decimal ProductionCost
    {
        get { return productionCost; }
    }

    public decimal SellPrice
    {
        get { return sellPrice; }
    }

    public string Temperature
    {
        get { return temperature; }
    }

    // Static attribute to track total menu positions
    public static int TotalMenuPositions { get; private set; } = 0;

    // Class extent (list of all menu positions)
    private static List<MenuPosition> MenuPositions = new List<MenuPosition>();

    // Constant for Desired Profit Margin
    public const decimal DesiredProfitMargin = 0.09m;

    // Derived attribute
    public decimal ActualProfitMargin
    {
        get
        {
            if (sellPrice > 0)
            {
                return (sellPrice - productionCost) / sellPrice;
            }
            return 0; // Avoid division by zero
        }
    }

    // Constructor
    public MenuPosition(string name, decimal productionCost, decimal sellPrice, string temperature)
    {
        // Validate inputs
        this.name = Validator.ValidateNonEmptyString(name, nameof(Name));
        this.temperature = Validator.ValidateNonEmptyString(temperature, nameof(Temperature));

        if (productionCost < 0)
        {
            throw new ArgumentException($"{nameof(ProductionCost)} cannot be negative.");
        }

        if (sellPrice <= 0)
        {
            throw new ArgumentException($"{nameof(SellPrice)} must be greater than zero.");
        }

        this.productionCost = productionCost;
        this.sellPrice = sellPrice;

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
        Console.WriteLine($"Desired Profit Margin: {DesiredProfitMargin:P}");
        Console.WriteLine($"Actual Profit Margin: {ActualProfitMargin:P}");
    }

    // Static method to display all menu positions
    public static void DisplayAllMenuPositions()
    {
        Console.WriteLine("\n--- All Menu Positions ---");
        foreach (var menuPosition in MenuPositions)
        {
            menuPosition.DisplayMenuPositionInfo();
            Console.WriteLine();
        }
    }

    // Serialization method
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

    // Deserialization method
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


