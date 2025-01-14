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

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class Drink
{
    // Enum for Drinkware Type
    public enum DrinkwareTypeEnum
    {
        Glass,
        Mug,
        WineGlass,
        PlasticGlass
    }

    // Private fields
    private DrinkwareTypeEnum drinkwareType; // Type of drinkware
    private bool ifSparkling; // Whether the drink is sparkling or not

    // Public read-only properties
    public DrinkwareTypeEnum DrinkwareType
    {
        get { return drinkwareType; }
    }

    public bool IfSparkling
    {
        get { return ifSparkling; }
    }

    // Static attribute to count total drinks
    public static int TotalDrinks { get; private set; } = 0;

    // Class extent to store all drink instances
    private static List<Drink> Drinks = new List<Drink>();

    // Constructor
    public Drink(DrinkwareTypeEnum drinkwareType, bool ifSparkling)
    {
        // Validate drinkware type using Evaluator (assumes it throws if invalid)
        this.drinkwareType = drinkwareType;

        // Assign other fields
        this.ifSparkling = ifSparkling;

        // Increment static count and add to class extent
        TotalDrinks++;
        Drinks.Add(this);
    }

    // Method to display drink information
    public void DisplayDrinkInfo()
    {
        Console.WriteLine($"Drinkware Type: {DrinkwareType}");
        Console.WriteLine($"Is Sparkling: {(IfSparkling ? "Yes" : "No")}");
    }

    // Static method to display all drinks
    public static void DisplayAllDrinks()
    {
        Console.WriteLine("\n--- All Drinks ---");
        foreach (var drink in Drinks)
        {
            drink.DisplayDrinkInfo();
            Console.WriteLine();
        }
    }

    // Serialization method
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

    // Deserialization method
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


