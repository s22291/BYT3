using System;
using System.Collections.Generic;

public class Ingredient
{
    // Basic Attribute
    public string Type { get; set; } // The type of ingredient (e.g., "Spice", "Vegetable")

    // MultiValue Attribute
    public List<string> CountryOfOrigin { get; set; } // List of countries where the ingredient originates (1 to 3)

    // MultiValue Attribute
    public List<decimal> PriceOnMarket { get; set; } // List of market prices for the ingredient

    // Optional Attribute
    public string Supplier { get; set; } // Optional supplier name

    // Static Attribute
    public static int TotalIngredients { get; private set; } = 0; // Total number of Ingredient instances created

    // Class Extent
    private static List<Ingredient> Ingredients = new List<Ingredient>(); // Stores all Ingredient instances

    // Complex Attribute
    public DateTime DateAddedToInventory { get; set; } // The date when the ingredient was added to inventory

    // Derived Attribute
    public decimal AveragePrice
    {
        get
        {
            if (PriceOnMarket.Count > 0)
            {
                decimal total = 0;
                foreach (var price in PriceOnMarket)
                {
                    total += price;
                }
                return total / PriceOnMarket.Count; // Calculate the average price
            }
            return 0;
        }
    }

    // Constructor
    public Ingredient(string type, List<string> countryOfOrigin, List<decimal> priceOnMarket, DateTime dateAddedToInventory, string supplier = null)
    {
        if (countryOfOrigin.Count < 1 || countryOfOrigin.Count > 3)
        {
            throw new ArgumentException("Country of origin must have between 1 and 3 entries.");
        }

        if (priceOnMarket.Count != countryOfOrigin.Count)
        {
            throw new ArgumentException("Price on market must have the same number of entries as country of origin.");
        }

        Type = type;
        CountryOfOrigin = countryOfOrigin;
        PriceOnMarket = priceOnMarket;
        DateAddedToInventory = dateAddedToInventory;
        Supplier = supplier; // Optional attribute

        // Increment static count and add to class extent
        TotalIngredients++;
        Ingredients.Add(this);
    }

    // Method to display ingredient information
    public void DisplayIngredientInfo()
    {
        Console.WriteLine($"Ingredient Type: {Type}");
        Console.WriteLine($"Date Added to Inventory: {DateAddedToInventory:yyyy-MM-dd}");
        Console.WriteLine($"Supplier: {Supplier ?? "None"}");
        Console.WriteLine($"Average Price: {AveragePrice:C}");
        Console.WriteLine("Countries of Origin and Prices:");

        for (int i = 0; i < CountryOfOrigin.Count; i++)
        {
            Console.WriteLine($"  - {CountryOfOrigin[i]}: {PriceOnMarket[i]:C}");
        }
    }

    // Static Method to Display All Ingredients
    public static void DisplayAllIngredients()
    {
        Console.WriteLine("\n--- All Ingredients ---");
        foreach (var ingredient in Ingredients)
        {
            ingredient.DisplayIngredientInfo();
            Console.WriteLine();
        }
    }
}

