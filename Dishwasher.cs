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
    // Private backing field for validation
    private double avgCleanedDishesPerMinute;

    // Public property with validation
    public double AvgCleanedDishesPerMinute
    {
        get { return avgCleanedDishesPerMinute; }
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Average cleaned dishes per minute must be greater than zero.");
            }
            avgCleanedDishesPerMinute = value;
        }
    }

    // Optional Attribute
    public DateTime? ReprimandFromCooksDate { get; private set; } // Nullable reprimand date

    // Static Attribute
    public static int TotalDishwashers { get; private set; } = 0; // Total number of Dishwasher instances

    // Class Extent
    private static List<Dishwasher> Dishwashers = new List<Dishwasher>(); // Stores all Dishwasher objects

    // Constructor for initializing without reprimand date
    public Dishwasher(double avgCleanedDishesPerMinute)
    {
        AvgCleanedDishesPerMinute = avgCleanedDishesPerMinute; // Validation happens here
        ReprimandFromCooksDate = null;

        // Increment static count and add to class extent
        TotalDishwashers++;
        Dishwashers.Add(this);
    }

    // Constructor for initializing with reprimand date
    public Dishwasher(double avgCleanedDishesPerMinute, DateTime reprimandFromCooksDate)
        : this(avgCleanedDishesPerMinute)
    {
        ReprimandFromCooksDate = reprimandFromCooksDate;
    }

    // Method to update AvgCleanedDishesPerMinute
    public void UpdateAvgCleanedDishesPerMinute(double newAvg)
    {
        AvgCleanedDishesPerMinute = newAvg; // Validation happens in the setter
    }

    // Method to update ReprimandFromCooksDate
    public void UpdateReprimandFromCooksDate(DateTime? newDate)
    {
        ReprimandFromCooksDate = newDate; // No validation needed for nullable date
    }

    // Method to display dishwasher information
    public void DisplayDishwasherInfo()
    {
        Console.WriteLine($"Average Cleaned Dishes Per Minute: {AvgCleanedDishesPerMinute}");
        Console.WriteLine($"Reprimand From Cooks Date: {ReprimandFromCooksDate?.ToString("d") ?? "None"}");
    }

    // Static Method to Display All Dishwashers
    public static void DisplayAllDishwashers()
    {
        Console.WriteLine("\n--- All Dishwashers ---");
        foreach (var dishwasher in Dishwashers)
        {
            dishwasher.DisplayDishwasherInfo();
            Console.WriteLine();
        }
    }

    // Serialization Method
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

    // Deserialization Method
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
