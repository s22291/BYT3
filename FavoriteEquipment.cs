/** \file     FavoriteEquipment.cs
*  \author    Aslan,Julio,Mohammad,Wiktor
*  \version   Final
*  \date      2024
*  \bug       No bugs so far
*  \copyright Polish and Japanies Information Technology 
*/

using System;
using System.Collections.Generic;

public class FavoriteEquipment
{
    // Basic Attribute
    public string KnifeSize { get; set; } // Preferred knife size (e.g., "6\"", "8\"", "10\"")

    // Optional Attribute
    public string CuttingBoardMaterial { get; set; } // Nullable cutting board material to make it optional

    // MultiValue Attribute
    public List<string> FavoriteBrands { get; set; } = new List<string>(); // List of preferred equipment brands

    // Static Attribute
    public static int TotalFavoriteEquipments { get; private set; } = 0; // Total number of FavoriteEquipment objects created

    // Class Extent
    private static List<FavoriteEquipment> Equipments = new List<FavoriteEquipment>(); // Stores all FavoriteEquipment instances

    // Complex Attribute
    public DateTime LastUpdated { get; set; } // Date the equipment preference was last updated

    // Derived Attribute
    public string EquipmentDescription
    {
        get
        {
            return $"{KnifeSize} knife with a {CuttingBoardMaterial?.ToLower() ?? "no"} cutting board"; // A readable description of the equipment
        }
    }

    // Constructor
    public FavoriteEquipment(string knifeSize, string cuttingBoardMaterial, DateTime lastUpdated)
    {
        // Validate knife size
        if (knifeSize != "6\"" && knifeSize != "8\"" && knifeSize != "10\"")
        {
            throw new ArgumentException("Invalid knife size. Choose '6\"', '8\"', or '10\"'.");
        }

        KnifeSize = knifeSize;
        CuttingBoardMaterial = cuttingBoardMaterial; // Cutting board material is optional
        LastUpdated = lastUpdated;

        // Increment static count and add to class extent
        TotalFavoriteEquipments++;
        Equipments.Add(this);
    }

    // Method to display favorite equipment information
    public void DisplayEquipmentInfo()
    {
        Console.WriteLine($"Knife Size: {KnifeSize}");
        Console.WriteLine($"Cutting Board Material: {CuttingBoardMaterial ?? "None"}"); // Handles the optional attribute
        Console.WriteLine($"Last Updated: {LastUpdated:yyyy-MM-dd}");
        Console.WriteLine($"Equipment Description: {EquipmentDescription}");
        Console.WriteLine("Favorite Brands: " + (FavoriteBrands.Count > 0 ? string.Join(", ", FavoriteBrands) : "None"));
    }

    // Static Method to Display All FavoriteEquipment Objects
    public static void DisplayAllEquipments()
    {
        Console.WriteLine("\n--- All Favorite Equipment ---");
        foreach (var equipment in Equipments)
        {
            equipment.DisplayEquipmentInfo();
            Console.WriteLine();
        }
    }
}
