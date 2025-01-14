/** \file     Mixologist.cs
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


public class Mixologist
{
    // Private backing field for OwnsUniform
    private bool ownsUniform;

    // Basic Attribute with validation
    public bool OwnsUniform
    {
        get { return ownsUniform; }
        private set
        {
            // Validate that the mixologist has at least 10 years of experience
            if (value == true && YearsOfExperience < 10)
            {
                throw new InvalidOperationException("A mixologist cannot own a uniform unless they have at least 10 years of experience.");
            }
            ownsUniform = value;
        }
    }

    // Private backing field for DateHired
    private DateTime dateHired;

    // Complex Attribute with validation
    public DateTime DateHired
    {
        get { return dateHired; }
        private set
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException("DateHired cannot be in the future.");
            }
            dateHired = value;
        }
    }

    // Derived Attribute
    public int YearsOfExperience
    {
        get { return DateTime.Now.Year - DateHired.Year; } // Calculate years of experience
    }

    // Static Attribute
    public static int TotalMixologists { get; private set; } = 0; // Total number of Mixologists

    // Class Extent
    private static List<Mixologist> Mixologists = new List<Mixologist>(); // Stores all Mixologist instances

    // Constructor
    public Mixologist(DateTime dateHired, bool ownsUniform = false)
    {
        DateHired = dateHired;      // Validate and set DateHired
        OwnsUniform = ownsUniform; // Validate and set OwnsUniform

        // Increment static count and add to class extent
        TotalMixologists++;
        Mixologists.Add(this);
    }

    // Method to update OwnsUniform
    public void UpdateOwnsUniform(bool ownsUniform)
    {
        OwnsUniform = ownsUniform; // Revalidate and set OwnsUniform
    }

    // Method to display mixologist information
    public void DisplayMixologistInfo()
    {
        Console.WriteLine($"Date Hired: {DateHired:yyyy-MM-dd}");
        Console.WriteLine($"Years of Experience: {YearsOfExperience}");
        Console.WriteLine($"Owns Uniform: {(OwnsUniform ? "Yes" : "No")}");
    }

    // Static Method to Display All Mixologists
    public static void DisplayAllMixologists()
    {
        Console.WriteLine("\n--- All Mixologists ---");
        foreach (var mixologist in Mixologists)
        {
            mixologist.DisplayMixologistInfo();
            Console.WriteLine();
        }
    }

    // Serialization Method
    public static void SaveToFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Mixologist>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Mixologists);
            }
            Console.WriteLine("Mixologists saved to file successfully.");
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
            XmlSerializer serializer = new XmlSerializer(typeof(List<Mixologist>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Mixologists = (List<Mixologist>?)serializer.Deserialize(reader) ?? new List<Mixologist>();
            }
            Console.WriteLine("Mixologists loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}

