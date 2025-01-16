/** \file     Drink.cs
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

public class Drink
{
    // -----------------------------------------------------
    // Enum for Drinkware Type
    // -----------------------------------------------------
    public enum DrinkwareTypeEnum
    {
        Glass,
        Mug,
        WineGlass,
        PlasticGlass
    }

    // -----------------------------------------------------
    // Private backing fields
    // -----------------------------------------------------
    private DrinkwareTypeEnum drinkwareType;
    private bool ifSparkling;

    // -----------------------------------------------------
    // Original properties (private setters).
    // We mark them [XmlIgnore] so the serializer does NOT
    // try to set them directly (which would fail due to private setters).
    // -----------------------------------------------------
    
    [XmlIgnore]
    public DrinkwareTypeEnum DrinkwareType
    {
        get => drinkwareType;
        private set
        {
            // If you had custom validation logic, call it here
            // For now, we assume the enum is always valid
            drinkwareType = value;
        }
    }

    [XmlIgnore]
    public bool IfSparkling
    {
        get => ifSparkling;
        private set
        {
            ifSparkling = value;
        }
    }

    // -----------------------------------------------------
    // Bridge properties for XML Serialization.
    // They have public getters/setters so the serializer
    // can set them. Inside, they call the original
    // properties (which enforce any validation logic).
    // -----------------------------------------------------
    
    [XmlElement("DrinkwareType")]
    public DrinkwareTypeEnum DrinkwareTypeForXml
    {
        get => drinkwareType;
        set => DrinkwareType = value; // calls private setter
    }

    [XmlElement("IfSparkling")]
    public bool IfSparklingForXml
    {
        get => ifSparkling;
        set => IfSparkling = value;   // calls private setter
    }

    // -----------------------------------------------------
    // Static attribute & class extent
    // -----------------------------------------------------
    public static int TotalDrinks { get; private set; } = 0;
    private static List<Drink> Drinks = new List<Drink>();

    // -----------------------------------------------------
    // Parameterless constructor (REQUIRED by XML serializer).
    // Provide safe defaults that won't break logic.
    // Do NOT increment TotalDrinks here (avoid double-count).
    // -----------------------------------------------------
    public Drink()
    {
        // Provide default values:
        drinkwareType = DrinkwareTypeEnum.Glass;
        ifSparkling = false;
    }

    // -----------------------------------------------------
    // Main constructor
    // -----------------------------------------------------
    public Drink(DrinkwareTypeEnum drinkwareType, bool ifSparkling)
    {
        DrinkwareType = drinkwareType;  // calls private setter
        IfSparkling = ifSparkling;      // calls private setter

        TotalDrinks++;
        Drinks.Add(this);
    }

    // -----------------------------------------------------
    // Method to display drink information
    // -----------------------------------------------------
    public void DisplayDrinkInfo()
    {
        Console.WriteLine($"Drinkware Type: {DrinkwareType}");
        Console.WriteLine($"Is Sparkling: {(IfSparkling ? "Yes" : "No")}");
    }

    // -----------------------------------------------------
    // Static method to display all drinks
    // -----------------------------------------------------
    public static void DisplayAllDrinks()
    {
        Console.WriteLine("\n--- All Drinks ---");
        foreach (var drink in Drinks)
        {
            drink.DisplayDrinkInfo();
            Console.WriteLine();
        }
    }

    // -----------------------------------------------------
    // Serialization method
    // -----------------------------------------------------
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

    // -----------------------------------------------------
    // Deserialization method
    // -----------------------------------------------------
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



