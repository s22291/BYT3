using System;
using System.Collections.Generic;

public class Ingredient
{
    // Properties of the Ingredient class
    public string Type { get; set; }                             // The type of ingredient (e.g., "Spice", "Vegetable")
    public List<string> CountryOfOrigin { get; set; }            // List of countries of origin (1 to 3)
    public List<decimal> PriceOnMarket { get; set; }             // List of market prices corresponding to countries (1 to 3)

    // Constructor to initialize the Ingredient class
    public Ingredient(string type, List<string> countryOfOrigin, List<decimal> priceOnMarket)
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
    }

    // Method to display ingredient information
    public void DisplayIngredientInfo()
    {
        Console.WriteLine($"Ingredient Type: {Type}");
        Console.WriteLine("Countries of Origin and Prices:");

        for (int i = 0; i < CountryOfOrigin.Count; i++)
        {
            Console.WriteLine($"  - {CountryOfOrigin[i]}: {PriceOnMarket[i]:C}");
        }
    }
}
