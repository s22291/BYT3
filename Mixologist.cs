/** \file     Mixologist.cs
*  \author    Aslan,Julio,Mohammad,Wiktor
*  \version   Final
*  \date      2024
*  \bug       No bugs so far
*  \copyright Polish and Japanies Information Technology 
*/

using System;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class Mixologist
{
    // Basic Attribute
    public bool OwnsUniform { get; set; } // Indicates whether the mixologist owns a uniform

    // Optional Attribute
    public string Certification { get; set; } // Optional certification (e.g., "Bartending License")

    // MultiValue Attribute
    public List<string> SignatureDrinks { get; set; } = new List<string>(); // List of signature drinks created by the mixologist

    // Static Attribute
    public static int TotalMixologists { get; private set; } = 0; // Total number of Mixologists

    // Class Extent
    private static List<Mixologist> Mixologists = new List<Mixologist>(); // Stores all Mixologist instances

    // Complex Attribute
    public DateTime DateHired { get; set; } // Date the mixologist was hired

    // Derived Attribute
    public int YearsOfExperience
    {
        get { return DateTime.Now.Year - DateHired.Year; } // Calculate years of experience
    }

    // Constructor
    public Mixologist(bool ownsUniform, string certification, DateTime dateHired)
    {
        OwnsUniform = ownsUniform;
        Certification = certification;
        DateHired = dateHired;

        // Increment static count and add to class extent
        TotalMixologists++;
        Mixologists.Add(this);
    }

    // Method to display mixologist information
    public void DisplayMixologistInfo()
    {
        Console.WriteLine($"Owns Uniform: {(OwnsUniform ? "Yes" : "No")}");
        Console.WriteLine($"Certification: {Certification ?? "None"}"); // Handles optional certification
        Console.WriteLine($"Date Hired: {DateHired:yyyy-MM-dd}");
        Console.WriteLine($"Years of Experience: {YearsOfExperience}");
        Console.WriteLine("Signature Drinks: " + (SignatureDrinks.Count > 0 ? string.Join(", ", SignatureDrinks) : "None"));
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
