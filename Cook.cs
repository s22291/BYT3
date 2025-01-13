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
    // Basic Attribute
    public string Role { get; set; }        // The role or specialization of the cook (e.g., "Pastry Chef", "Grill Cook")

    // Optional Attribute
    public bool? OwnsUniform { get; set; }  // Nullable to represent optional ownership of a uniform

    // MultiValue Attribute
    public List<string> FavoriteRecipes { get; set; } = new List<string>(); // List of the cook's favorite recipes

    // Static Attribute
    public static int TotalCooks { get; private set; } = 0; // Total number of cooks

    // Class Extent
    private static List<Cook> Cooks = new List<Cook>(); // Stores all Cook objects

    // Complex Attribute
    public DateTime DateHired { get; set; } // Complex attribute to store the hire date of the cook

    // Derived Attribute
    public int YearsOfExperience
    {
        get { return DateTime.Now.Year - DateHired.Year; } // Calculate years of experience based on hire date
    }

    // Constructor
    public Cook(string role, bool? ownsUniform, DateTime dateHired)
    {
        Role = role;
        OwnsUniform = ownsUniform;
        DateHired = dateHired;

        // Increment static count and add to class extent
        TotalCooks++;
        Cooks.Add(this);
    }

    // Method to display cook information
    public void DisplayCookInfo()
    {
        Console.WriteLine($"Role: {Role}");
        Console.WriteLine($"Owns Uniform: {(OwnsUniform.HasValue ? (OwnsUniform.Value ? "Yes" : "No") : "Unknown")}");
        Console.WriteLine($"Date Hired: {DateHired:yyyy-MM-dd}");
        Console.WriteLine($"Years of Experience: {YearsOfExperience}");
        Console.WriteLine("Favorite Recipes: " + (FavoriteRecipes.Count > 0 ? string.Join(", ", FavoriteRecipes) : "None"));
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
            }
            Console.WriteLine("Cooks loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}
