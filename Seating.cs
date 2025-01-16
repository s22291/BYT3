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
    // -----------------------------------------------------
    // Enum for seating types (unchanged)
    // -----------------------------------------------------
    public enum SeatingTypeEnum
    {
        CounterSeating,
        TableSeating
    }

    // -----------------------------------------------------
    // Private backing fields
    // -----------------------------------------------------
    private int number;                   // Unique ID
    private SeatingTypeEnum seatingType;  // Type of seating
    private bool ifInside;                // True if inside
    private string? specialNote;          // Optional note

    // -----------------------------------------------------
    // Original Properties (private setters).
    // Mark them [XmlIgnore] so the serializer won't try
    // to call the private setters directly.
    // -----------------------------------------------------
    
    [XmlIgnore]
    public int Number
    {
        get => number;
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Number must be greater than zero.");
            }
            number = value;
        }
    }

    [XmlIgnore]
    public SeatingTypeEnum SeatingType
    {
        get => seatingType;
        private set => seatingType = value;
    }

    [XmlIgnore]
    public bool IfInside
    {
        get => ifInside;
        private set => ifInside = value;
    }

    [XmlIgnore]
    public string? SpecialNote
    {
        get => specialNote;
        private set => specialNote = value;
    }

    // -----------------------------------------------------
    // Bridge properties for XML Serialization.
    // They have public getters/setters so the serializer
    // can set them. They call the original property
    // to preserve any validation logic.
    // -----------------------------------------------------

    [XmlElement("Number")]
    public int NumberForXml
    {
        get => number;
        set => Number = value;  // Calls the private setter (with validation)
    }

    [XmlElement("SeatingType")]
    public SeatingTypeEnum SeatingTypeForXml
    {
        get => seatingType;
        set => SeatingType = value;  // Calls the private setter
    }

    [XmlElement("IfInside")]
    public bool IfInsideForXml
    {
        get => ifInside;
        set => IfInside = value;    // Calls the private setter
    }

    [XmlElement("SpecialNote")]
    public string? SpecialNoteForXml
    {
        get => specialNote;
        set => SpecialNote = value; // Calls the private setter
    }

    // -----------------------------------------------------
    // Static attribute & Class extent
    // -----------------------------------------------------
    public static int TotalSeatings { get; private set; } = 0;
    private static List<Seating> Seatings = new List<Seating>();

    // -----------------------------------------------------
    // Parameterless constructor (REQUIRED by XML serializer).
    // Provide safe defaults. DO NOT increment `TotalSeatings`
    // here to avoid double-counting when deserializing.
    // -----------------------------------------------------
    public Seating()
    {
        // Provide valid defaults that won't break validation
        number = 1;
        seatingType = SeatingTypeEnum.TableSeating;
        ifInside = false;
        specialNote = null;
    }

    // -----------------------------------------------------
    // Constructor with special note
    // -----------------------------------------------------
    public Seating(int number, SeatingTypeEnum seatingType, bool ifInside, string? specialNote)
    {
        Number = number;         // calls the private setter (validation)
        SeatingType = seatingType;
        IfInside = ifInside;
        SpecialNote = specialNote;

        TotalSeatings++;
        Seatings.Add(this);
    }

    // -----------------------------------------------------
    // Constructor without special note
    // -----------------------------------------------------
    public Seating(int number, SeatingTypeEnum seatingType, bool ifInside)
        : this(number, seatingType, ifInside, null)
    {
    }

    // -----------------------------------------------------
    // Method to display seating info
    // -----------------------------------------------------
    public void DisplaySeatingInfo()
    {
        Console.WriteLine($"Seating Number: {Number}");
        Console.WriteLine($"Seating Type: {SeatingType}");
        Console.WriteLine($"Located Inside: {(IfInside ? "Yes" : "No")}");
        Console.WriteLine($"Special Note: {SpecialNote ?? "None"}");
    }

    // -----------------------------------------------------
    // Static method to display all seatings
    // -----------------------------------------------------
    public static void DisplayAllSeatings()
    {
        Console.WriteLine("\n--- All Seatings ---");
        foreach (var seating in Seatings)
        {
            seating.DisplaySeatingInfo();
            Console.WriteLine();
        }
    }

    // -----------------------------------------------------
    // Serialization / Deserialization
    // -----------------------------------------------------
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
