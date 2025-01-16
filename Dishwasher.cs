/** \file     Dishwasher.cs
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

public class Dishwasher
{
    // -----------------------------------------------------
    // Private backing fields
    // -----------------------------------------------------
    private double avgCleanedDishesPerMinute;
    private DateTime? reprimandFromCooksDate;

    // -----------------------------------------------------
    // Original properties (private setters).
    // Mark them [XmlIgnore], so the serializer won't
    // try to call these private setters directly.
    // -----------------------------------------------------

    [XmlIgnore]
    public double AvgCleanedDishesPerMinute
    {
        get { return avgCleanedDishesPerMinute; }
        private set
        {
            if (value <= 0)
                throw new ArgumentException("Average cleaned dishes per minute must be greater than zero.");
            avgCleanedDishesPerMinute = value;
        }
    }

    [XmlIgnore]
    public DateTime? ReprimandFromCooksDate
    {
        get { return reprimandFromCooksDate; }
        private set
        {
            // No particular validation for nullable
            reprimandFromCooksDate = value;
        }
    }

    // -----------------------------------------------------
    // Bridge properties for XML Serialization.
    // They have public setters so the serializer can set them.
    // They read/write the same private fields through
    // the original private-set properties (within the same class).
    // -----------------------------------------------------

    [XmlElement("AvgCleanedDishesPerMinute")]
    public double AvgCleanedDishesPerMinuteForXml
    {
        get => avgCleanedDishesPerMinute;
        set => AvgCleanedDishesPerMinute = value; // uses the original property
    }

    [XmlElement("ReprimandFromCooksDate")]
    public DateTime? ReprimandFromCooksDateForXml
    {
        get => reprimandFromCooksDate;
        set => ReprimandFromCooksDate = value;    // uses the original property
    }

    // -----------------------------------------------------
    // Static attributes & class extent
    // -----------------------------------------------------
    public static int TotalDishwashers { get; private set; } = 0; // total number of Dishwasher instances

    private static List<Dishwasher> Dishwashers = new List<Dishwasher>(); // stores all Dishwasher objects

    // -----------------------------------------------------
    // Parameterless constructor (REQUIRED by XML serializer).
    // Provide safe defaults that won't break validation.
    // DO NOT increment TotalDishwashers here (avoids double-count).
    // -----------------------------------------------------
    public Dishwasher()
    {
        // Provide defaults that won't throw an exception
        avgCleanedDishesPerMinute = 1.0;   // Must be > 0
        reprimandFromCooksDate = null;
    }

    // -----------------------------------------------------
    // Main Constructors
    // -----------------------------------------------------
    public Dishwasher(double avgCleanedDishesPerMinute)
    {
        AvgCleanedDishesPerMinute = avgCleanedDishesPerMinute;  // validation in setter
        ReprimandFromCooksDate = null;

        TotalDishwashers++;
        Dishwashers.Add(this);
    }

    public Dishwasher(double avgCleanedDishesPerMinute, DateTime reprimandFromCooksDate)
        : this(avgCleanedDishesPerMinute)
    {
        ReprimandFromCooksDate = reprimandFromCooksDate;
    }

    // -----------------------------------------------------
    // Update Methods
    // -----------------------------------------------------
    public void UpdateAvgCleanedDishesPerMinute(double newAvg)
    {
        AvgCleanedDishesPerMinute = newAvg; // validation in setter
    }

    public void UpdateReprimandFromCooksDate(DateTime? newDate)
    {
        ReprimandFromCooksDate = newDate;   // no validation needed for nullable
    }

    // -----------------------------------------------------
    // Display Methods
    // -----------------------------------------------------
    public void DisplayDishwasherInfo()
    {
        Console.WriteLine($"Average Cleaned Dishes Per Minute: {AvgCleanedDishesPerMinute}");
        Console.WriteLine($"Reprimand From Cooks Date: {ReprimandFromCooksDate?.ToString("d") ?? "None"}");
    }

    public static void DisplayAllDishwashers()
    {
        Console.WriteLine("\n--- All Dishwashers ---");
        foreach (var dishwasher in Dishwashers)
        {
            dishwasher.DisplayDishwasherInfo();
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
            XmlSerializer serializer = new XmlSerializer(typeof(List<Dishwasher>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Dishwashers);
            }
            Console.WriteLine("Dishwashers saved to file successfully.");
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
            XmlSerializer serializer = new XmlSerializer(typeof(List<Dishwasher>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Dishwashers = (List<Dishwasher>?)serializer.Deserialize(reader) ?? new List<Dishwasher>();
            }
            Console.WriteLine("Dishwashers loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}
