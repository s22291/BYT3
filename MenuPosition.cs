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
    // -----------------------------------------------------
    // Private backing fields
    // -----------------------------------------------------
    private string name;
    private decimal productionCost;
    private decimal sellPrice;
    private string temperature;

    // -----------------------------------------------------
    // Original Properties (private setters).
    // Mark them [XmlIgnore] so the serializer won't try
    // to call these private setters directly.
    // -----------------------------------------------------
    
    [XmlIgnore]
    public string Name
    {
        get => name;
        private set
        {
            // Reuse your Validator logic
            name = Validator.ValidateNonEmptyString(value, nameof(Name));
        }
    }

    [XmlIgnore]
    public decimal ProductionCost
    {
        get => productionCost;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(ProductionCost)} cannot be negative.");
            }
            productionCost = value;
        }
    }

    [XmlIgnore]
    public decimal SellPrice
    {
        get => sellPrice;
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException($"{nameof(SellPrice)} must be greater than zero.");
            }
            sellPrice = value;
        }
    }

    [XmlIgnore]
    public string Temperature
    {
        get => temperature;
        private set
        {
            // Reuse your Validator logic
            temperature = Validator.ValidateNonEmptyString(value, nameof(Temperature));
        }
    }

    // -----------------------------------------------------
    // Derived attribute
    // Typically marked [XmlIgnore] if we don't want to
    // serialize it or if it has no setter.
    // -----------------------------------------------------
    [XmlIgnore]
    public decimal ActualProfitMargin
    {
        get
        {
            if (SellPrice > 0)
            {
                return (SellPrice - ProductionCost) / SellPrice;
            }
            return 0;
        }
    }

    // -----------------------------------------------------
    // Constants / Static
    // -----------------------------------------------------
    public const decimal DesiredProfitMargin = 0.09m;

    public static int TotalMenuPositions { get; private set; } = 0;

    private static List<MenuPosition> MenuPositions = new List<MenuPosition>();

    // -----------------------------------------------------
    // Bridge properties for XML serialization.
    // They have public getters/setters so the serializer
    // can set them. Inside, they call the original
    // properties (with private setters), preserving
    // all validation logic.
    // -----------------------------------------------------
    
    [XmlElement("Name")]
    public string NameForXml
    {
        get => name;
        set => Name = value; // triggers private validation
    }

    [XmlElement("ProductionCost")]
    public decimal ProductionCostForXml
    {
        get => productionCost;
        set => ProductionCost = value; // triggers private validation
    }

    [XmlElement("SellPrice")]
    public decimal SellPriceForXml
    {
        get => sellPrice;
        set => SellPrice = value;      // triggers private validation
    }

    [XmlElement("Temperature")]
    public string TemperatureForXml
    {
        get => temperature;
        set => Temperature = value;    // triggers private validation
    }

    // -----------------------------------------------------
    // Parameterless constructor (REQUIRED by XML serializer).
    // Provide safe defaults that won't fail validation.
    // Do NOT increment `TotalMenuPositions` here to avoid
    // double-counting on deserialization.
    // -----------------------------------------------------
    public MenuPosition()
    {
        // Safe defaults
        name = "Undefined";
        productionCost = 0m;
        sellPrice = 1m;   // Must be > 0
        temperature = "Cold";
    }

    // -----------------------------------------------------
    // Main constructor (original)
    // -----------------------------------------------------
    public MenuPosition(string name, decimal productionCost, decimal sellPrice, string temperature)
    {
        Name = name;                 // triggers validation
        ProductionCost = productionCost;
        SellPrice = sellPrice;
        Temperature = temperature;

        TotalMenuPositions++;
        MenuPositions.Add(this);
    }

    // -----------------------------------------------------
    // Method to display menu position information
    // -----------------------------------------------------
    public void DisplayMenuPositionInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Production Cost: {ProductionCost:C}");
        Console.WriteLine($"Sell Price: {SellPrice:C}");
        Console.WriteLine($"Temperature: {Temperature}");
        Console.WriteLine($"Desired Profit Margin: {DesiredProfitMargin:P}");
        Console.WriteLine($"Actual Profit Margin: {ActualProfitMargin:P}");
    }

    // -----------------------------------------------------
    // Static methods
    // -----------------------------------------------------
    public static void DisplayAllMenuPositions()
    {
        Console.WriteLine("\n--- All Menu Positions ---");
        foreach (var menuPosition in MenuPositions)
        {
            menuPosition.DisplayMenuPositionInfo();
            Console.WriteLine();
        }
    }

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

    public static void LoadFromFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<MenuPosition>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                MenuPositions = (List<MenuPosition>?)serializer.Deserialize(reader) 
                                ?? new List<MenuPosition>();
            }
            Console.WriteLine("Menu positions loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}


