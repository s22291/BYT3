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

    // --------------------------------------------------
    // 1) Private backing fields for all the properties.
    // --------------------------------------------------
    private RoleType role;
    private bool? ownsUniform;
    private string favoriteRecipe;

    // --------------------------------------------------
    // Original properties (private setters, protected).
    // We mark them [XmlIgnore] so the serializer DOES NOT
    // complain about the private setter.
    // --------------------------------------------------

    [XmlIgnore]
    public RoleType Role
    {
        get { return role; }
        private set { role = value; }
    }

    [XmlIgnore]
    public bool? OwnsUniform
    {
        get { return ownsUniform; }
        private set { ownsUniform = value; }
    }

    [XmlIgnore]
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

    // --------------------------------------------------
    // Bridge properties for XML serialization:
    //  - These are public, so the serializer is happy.
    //  - They read/write the same backing fields.
    // --------------------------------------------------

    [XmlElement("Role")]
    public RoleType RoleForXml
    {
        get => role;
        set => role = value;  // Set the same backing field
    }

    [XmlElement("OwnsUniform")]
    public bool? OwnsUniformForXml
    {
        get => ownsUniform;
        set => ownsUniform = value;  // Set the same backing field
    }

    [XmlElement("FavoriteRecipe")]
    public string FavoriteRecipeForXml
    {
        get => favoriteRecipe;
        set
        {
            // Reuse the logic from the main property if you want to keep
            // the same "Unavailable" default for empty strings:
            if (string.IsNullOrWhiteSpace(value))
            {
                favoriteRecipe = "Unavailable";
            }
            else
            {
                favoriteRecipe = value;
            }
        }
    }

    // --------------------------------------------------
    // Private static field to track total cooks
    // Class Extent (List of all cooks)
    // --------------------------------------------------
    private static int totalCooks = 0;
    private static List<Cook> Cooks = new List<Cook>();

    // Parameterless Constructor (Required for Serialization)
    public Cook()
    {
        // Default values for deserialization
        Role = RoleType.LineCook;
        OwnsUniform = null;
        FavoriteRecipe = "Unavailable";
    }

    // Main Constructor
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

    // Overloaded Constructor (Without Favorite Recipe)
    public Cook(RoleType role, bool? ownsUniform)
        : this(role, ownsUniform, "Unavailable") // Default to "Unavailable" for FavoriteRecipe
    {
    }

    // Method to Display Cook Information
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
        Console.WriteLine($"Total Cooks: {totalCooks}");
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
                var deserializedCooks = (List<Cook>?)serializer.Deserialize(reader) ?? new List<Cook>();

                // Clear existing data
                Cooks.Clear();
                Cooks.AddRange(deserializedCooks);

                // Update the counter
                totalCooks = Cooks.Count;
            }
            Console.WriteLine("Cooks loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}

