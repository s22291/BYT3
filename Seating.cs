/** \file     Seating.cs
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

public class Seating
{
    // Enum for seating types
    public enum SeatingTypeEnum
    {
        CounterSeating,
        TableSeating
    }

    // Private fields
    private int number; // Unique identifier for the seating (ID)
    private SeatingTypeEnum seatingType; // Type of seating
    private bool ifInside; // True if located inside, false otherwise
    private string? specialNote; // Optional note about the seating

    // Public read-only properties
    public int Number
    {
        get { return number; }
    }

    public SeatingTypeEnum SeatingType
    {
        get { return seatingType; }
    }

    public bool IfInside
    {
        get { return ifInside; }
    }

    public string? SpecialNote
    {
        get { return specialNote; }
    }

    // Static attribute
    public static int TotalSeatings { get; private set; } = 0;

    // Class extent (list of all seating instances)
    private static List<Seating> Seatings = new List<Seating>();

    // Constructor with special note
    public Seating(int number, SeatingTypeEnum seatingType, bool ifInside, string? specialNote)
    {
        if (number <= 0)
        {
            throw new ArgumentException("Number must be greater than zero.");
        }

        this.number = number;
        this.seatingType = seatingType;
        this.ifInside = ifInside;
        this.specialNote = specialNote;

        // Increment static count and add to the class extent
        TotalSeatings++;
        Seatings.Add(this);
    }

    // Constructor without special note
    public Seating(int number, SeatingTypeEnum seatingType, bool ifInside)
        : this(number, seatingType, ifInside, null) // Call the other constructor with null for specialNote
    {
    }

    // Method to display seating information
    public void DisplaySeatingInfo()
    {
        Console.WriteLine($"Seating Number: {Number}");
        Console.WriteLine($"Seating Type: {SeatingType}");
        Console.WriteLine($"Located Inside: {(IfInside ? "Yes" : "No")}");
        Console.WriteLine($"Special Note: {SpecialNote ?? "None"}");
    }

    // Static method to display all seatings
    public static void DisplayAllSeatings()
    {
        Console.WriteLine("\n--- All Seatings ---");
        foreach (var seating in Seatings)
        {
            seating.DisplaySeatingInfo();
            Console.WriteLine();
        }
    }

    // Serialization method
    public static void SaveToFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Seating>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Seatings);
            }
            Console.WriteLine("Seatings saved to file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    // Deserialization method
    public static void LoadFromFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Seating>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Seatings = (List<Seating>?)serializer.Deserialize(reader) ?? new List<Seating>();
            }
            Console.WriteLine("Seatings loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}
