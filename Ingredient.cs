/** \file     Ingredient.cs
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

public class Ingredient
{
    // -----------------------------------------------------
    // Private backing fields
    // -----------------------------------------------------
    private string type;
    private string country;
    private decimal price;

    // -----------------------------------------------------
    // Class extent & static counter
    // -----------------------------------------------------
    private static int totalIngredients = 0;
    private static List<Ingredient> ingredientsExtent = new List<Ingredient>();

    // -----------------------------------------------------
    // Original properties (private setters).
    // Mark them [XmlIgnore] so the serializer won't try
    // to call these private setters directly.
    // -----------------------------------------------------

    [XmlIgnore]
    public string Type
    {
        get => type;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Type cannot be null or empty.");
            type = value;
        }
    }

    [XmlIgnore]
    public string Country
    {
        get => country;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Country cannot be null or empty.");

            // Count how many times this country is already used
            int existingCountryCount = 0;
            foreach (var ingr in ingredientsExtent)
            {
                if (ingr.country.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    existingCountryCount++;
                }
            }

            // If this country already appears 3 times, throw an exception
            if (existingCountryCount >= 3)
            {
                throw new ArgumentException($"Cannot add ingredient for country '{value}' more than 3 times.");
            }

            country = value;
        }
    }

    [XmlIgnore]
    public decimal Price
    {
        get => price;
        private set
        {
            // Additional validation if desired, e.g. non-negative
            price = value;
        }
    }

    // -----------------------------------------------------
    // Derived attribute
    // We typically mark it [XmlIgnore] (no setter).
    // -----------------------------------------------------
    [XmlIgnore]
    public decimal AveragePrice => price;

    // -----------------------------------------------------
    // Public read-only static property & extent access
    // -----------------------------------------------------
    public static int TotalIngredients => totalIngredients;

    // -----------------------------------------------------
    // Bridge properties for XML serialization.
    // They have public getters/setters so the serializer
    // can set them. They point to the private-set original
    // properties, preserving your validation logic.
    // -----------------------------------------------------

    [XmlElement("Type")]
    public string TypeForXml
    {
        get => type;
        set => Type = value; // Calls the private setter
    }

    [XmlElement("Country")]
    public string CountryForXml
    {
        get => country;
        set => Country = value; // Calls the private setter
    }

    [XmlElement("Price")]
    public decimal PriceForXml
    {
        get => price;
        set => Price = value;   // Calls the private setter
    }

    // -----------------------------------------------------
    // Parameterless constructor (REQUIRED by XML serializer).
    // Provide safe defaults that won't violate validation.
    // Do NOT increment totalIngredients here — prevents
    // double-counting on deserialization.
    // -----------------------------------------------------
    public Ingredient()
    {
        // Safe defaults that won't fail validation checks
        type = "GenericIngredient";
        country = "GenericCountry";
        price = 1m;  // or any acceptable default
    }

    // -----------------------------------------------------
    // Main constructor with validation
    // -----------------------------------------------------
    public Ingredient(string type, string country, decimal price)
    {
        // This will trigger the logic in the private setters (via the public properties).
        Type = type;
        Country = country;
        Price = price;

        totalIngredients++;
        ingredientsExtent.Add(this);
    }

    // -----------------------------------------------------
    // Methods
    // -----------------------------------------------------
    public void DisplayIngredientInfo()
    {
        Console.WriteLine($"Ingredient Type: {Type}");
        Console.WriteLine($"Country of Origin: {Country}");
        Console.WriteLine($"Price: {Price:C}");
        Console.WriteLine($"Average Price (same as Price): {AveragePrice:C}");
    }

    public static void DisplayAllIngredients()
    {
        Console.WriteLine("\n--- All Ingredients ---");
        foreach (var ing in ingredientsExtent)
        {
            ing.DisplayIngredientInfo();
            Console.WriteLine();
        }
    }

    // -----------------------------------------------------
    // Serialization
    // -----------------------------------------------------
    public static void SaveToFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Ingredient>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, ingredientsExtent);
            }
            Console.WriteLine("Ingredients saved to file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    // -----------------------------------------------------
    // Deserialization
    // -----------------------------------------------------
    public static void LoadFromFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Ingredient>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                ingredientsExtent = (List<Ingredient>?)serializer.Deserialize(reader) 
                                    ?? new List<Ingredient>();
            }
            // Update the static counter to match the new list size
            totalIngredients = ingredientsExtent.Count;
            Console.WriteLine("Ingredients loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}



