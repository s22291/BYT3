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
    private string type;
    private string country;
    private decimal price;

    private static int totalIngredients = 0;
    private static List<Ingredient> ingredientsExtent = new List<Ingredient>();

    public string Type => type;
    public string Country => country;
    public decimal Price => price;

    public static int TotalIngredients => totalIngredients;

    // Derived attribute (average price doesn't make much sense if we only have one price per object,
    // but leaving here as a placeholder or example).
    public decimal AveragePrice => price;

    public Ingredient(string type, string country, decimal price)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Type cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be null or empty.");

        // Count how many times this country is already used
        int existingCountryCount = 0;
        foreach (var ingr in ingredientsExtent)
        {
            if (ingr.country.Equals(country, StringComparison.OrdinalIgnoreCase))
            {
                existingCountryCount++;
            }
        }

        // If this country already appears 3 times, throw an exception
        if (existingCountryCount >= 3)
        {
            throw new ArgumentException($"Cannot add ingredient for country '{country}' more than 3 times.");
        }

        // Otherwise, create the new ingredient
        this.type = type;
        this.country = country;
        this.price = price;

        totalIngredients++;
        ingredientsExtent.Add(this);
    }

    public void DisplayIngredientInfo()
    {
        Console.WriteLine($"Ingredient Type: {type}");
        Console.WriteLine($"Country of Origin: {country}");
        Console.WriteLine($"Price: {price:C}");
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

    public static void LoadFromFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Ingredient>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                ingredientsExtent = (List<Ingredient>)serializer.Deserialize(reader) ?? new List<Ingredient>();
            }
            totalIngredients = ingredientsExtent.Count;
            Console.WriteLine("Ingredients loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}


