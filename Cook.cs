/** \file     Cook.cs
*  \author    Aslan,Julio,Mohammad,Wiktor
*  \version   Final
*  \date      2024
*  \bug       No bugs so far
*  \copyright Polish and Japanies Information Technology 
*/

// Purpose: Define the Cook class with basic, optional, multi-value, static, complex, and derived attributes, as well as a constructor and methods to display cook information and all cooks.
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class Cook
{
    // Enum for allowed roles
    public enum RoleType
    {
        ExecutiveChef,
        SousChef,
        LineCook
    }

    // Basic Attribute
    private RoleType role;

    public RoleType Role
    {
        get { return role; }
        private set { role = value; }
    }

    // Optional Attribute
    public bool? OwnsUniform { get; set; }

    // Private backing field for FavoriteRecipe
    private string favoriteRecipe;

    public string FavoriteRecipe
    {
        get { return favoriteRecipe; }
        private set
        {
            // Validate if value is null, empty, or whitespace
            if (string.IsNullOrWhiteSpace(value))
            {
                favoriteRecipe = "Unavailable"; // Default value
            }
            else
            {
                favoriteRecipe = value;
            }
        }
    }

    // Private field to track total cooks (no public getter or setter)
    private static int totalCooks = 0;

    // Class Extent
    private static List<Cook> Cooks = new List<Cook>();

    // Constructor for when FavoriteRecipe is provided
    public Cook(RoleType role, bool? ownsUniform, string favoriteRecipe)
    {
        Role = role;
        OwnsUniform = ownsUniform;
        FavoriteRecipe = favoriteRecipe; // If null or empty, it will default to "Unavailable"

        // Increment private totalCooks counter
        totalCooks++;

        // Add to class extent
        Cooks.Add(this);
    }

    // Overloaded constructor for when FavoriteRecipe is NOT provided
    public Cook(RoleType role, bool? ownsUniform)
        : this(role, ownsUniform, "Unavailable") // Calls the main constructor with "Unavailable" as the default
    {
    }

    // Method to update Role
    public void UpdateRole(RoleType newRole)
    {
        Role = newRole;
    }

    // Method to update FavoriteRecipe
    public void UpdateFavoriteRecipe(string newFavoriteRecipe)
    {
        FavoriteRecipe = newFavoriteRecipe; // Validation happens in the setter
    }

    // Method to display cook information
    public void DisplayCookInfo()
    {
        Console.WriteLine($"Role: {Role}");
        Console.WriteLine($"Owns Uniform: {(OwnsUniform.HasValue ? (OwnsUniform.Value ? "Yes" : "No") : "Unknown")}");
        Console.WriteLine($"Favorite Recipe: {FavoriteRecipe}");
    }

    // Static Method to Display All Cooks
    public static void DisplayAllCooks()
    {
        Console.WriteLine("\n--- All Cooks ---");
        foreach (var cook in Cooks)
        {
            cook.DisplayCookInfo();
            Console.WriteLine();
        }

        // Display the total number of cooks
        Console.WriteLine($"Total Cooks: {GetTotalCooks()}");
    }

    // Private Method to Get Total Cooks
    private static int GetTotalCooks()
    {
        return totalCooks;
    }

    // Serialization Method
    public static void SaveToFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Cook>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Cooks);
            }
            Console.WriteLine("Cooks saved to file successfully.");
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
            XmlSerializer serializer = new XmlSerializer(typeof(List<Cook>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Cooks = (List<Cook>?)serializer.Deserialize(reader) ?? new List<Cook>();
                totalCooks = Cooks.Count; // Update the counter after loading
            }
            Console.WriteLine("Cooks loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}
