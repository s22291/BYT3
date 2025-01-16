/** \file     Waiter.cs
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

public class Waiter
{
    // -----------------------------------------------------
    // Private backing fields
    // -----------------------------------------------------
    private decimal avgTipsValuePerDay;
    private int averageCustomersServedDaily;

    // -----------------------------------------------------
    // Original properties (private setters).
    // Mark them [XmlIgnore] so the serializer won't try
    // to call the private setters directly.
    // -----------------------------------------------------
    
    [XmlIgnore]
    public decimal AvgTipsValuePerDay
    {
        get => avgTipsValuePerDay;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Average tips value per day cannot be negative.");
            }
            avgTipsValuePerDay = value;
        }
    }

    [XmlIgnore]
    public int AverageCustomersServedDaily
    {
        get => averageCustomersServedDaily;
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Average customers served daily must be greater than zero.");
            }
            averageCustomersServedDaily = value;
        }
    }

    // -----------------------------------------------------
    // Derived property (calculated, no setter).
    // Typically marked [XmlIgnore] unless you want to
    // serialize it too (then you'd make a bridging property).
    // -----------------------------------------------------
    [XmlIgnore]
    public decimal AverageTipsPerCustomer
    {
        get
        {
            if (averageCustomersServedDaily > 0)
            {
                return avgTipsValuePerDay / averageCustomersServedDaily;
            }
            return 0;
        }
    }

    // -----------------------------------------------------
    // Bridge properties for XML serialization.
    // They have public getters/setters so the serializer
    // can set them. They call the original private-setter
    // properties, preserving all validation logic.
    // -----------------------------------------------------
    
    [XmlElement("AvgTipsValuePerDay")]
    public decimal AvgTipsValuePerDayForXml
    {
        get => avgTipsValuePerDay;
        set => AvgTipsValuePerDay = value; // Uses the private setter
    }

    [XmlElement("AverageCustomersServedDaily")]
    public int AverageCustomersServedDailyForXml
    {
        get => averageCustomersServedDaily;
        set => AverageCustomersServedDaily = value; // Uses the private setter
    }

    // -----------------------------------------------------
    // Static attributes and class extent
    // -----------------------------------------------------
    public static int TotalWaiters { get; private set; } = 0;
    private static List<Waiter> Waiters = new List<Waiter>();

    // -----------------------------------------------------
    // Parameterless constructor (REQUIRED by XML serializer).
    // Provide safe defaults. DO NOT increment `TotalWaiters`
    // here (avoid double-counting on deserialization).
    // -----------------------------------------------------
    public Waiter()
    {
        // Safe defaults so validation won't fail
        avgTipsValuePerDay = 0.01m;
        averageCustomersServedDaily = 1;
    }

    // -----------------------------------------------------
    // Main constructor (the "real" one)
    // -----------------------------------------------------
    public Waiter(decimal avgTipsValuePerDay, int averageCustomersServedDaily)
    {
        if (avgTipsValuePerDay < 0)
        {
            throw new ArgumentException("Average tips value per day cannot be negative.");
        }
        if (averageCustomersServedDaily <= 0)
        {
            throw new ArgumentException("Average customers served daily must be greater than zero.");
        }

        this.avgTipsValuePerDay = avgTipsValuePerDay;
        this.averageCustomersServedDaily = averageCustomersServedDaily;

        TotalWaiters++;
        Waiters.Add(this);
    }

    // -----------------------------------------------------
    // Instance Methods
    // -----------------------------------------------------
    public void DisplayWaiterInfo()
    {
        Console.WriteLine($"Average Tips Value Per Day: {AvgTipsValuePerDay:C}");
        Console.WriteLine($"Average Customers Served Daily: {AverageCustomersServedDaily}");
        Console.WriteLine($"Average Tips Per Customer: {AverageTipsPerCustomer:C}");
    }

    // -----------------------------------------------------
    // Static Methods
    // -----------------------------------------------------
    public static void DisplayAllWaiters()
    {
        Console.WriteLine("\n--- All Waiters ---");
        foreach (var waiter in Waiters)
        {
            waiter.DisplayWaiterInfo();
            Console.WriteLine();
        }
    }

    public static void SaveToFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Waiter>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Waiters);
            }
            Console.WriteLine("Waiters saved to file successfully.");
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
            XmlSerializer serializer = new XmlSerializer(typeof(List<Waiter>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Waiters = (List<Waiter>?)serializer.Deserialize(reader) ?? new List<Waiter>();
            }
            Console.WriteLine("Waiters loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}
