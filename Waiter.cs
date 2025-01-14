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
    // Private fields (no direct external access)
    private decimal avgTipsValuePerDay; // Average value of tips per day
    private int averageCustomersServedDaily; // Average customers served daily

    // Public read-only properties
    public decimal AvgTipsValuePerDay
    {
        get { return avgTipsValuePerDay; } // Expose the value, but not modifiable
    }

    public int AverageCustomersServedDaily
    {
        get { return averageCustomersServedDaily; } // Expose the value, but not modifiable
    }

    // Static attribute (tracks total number of waiters)
    public static int TotalWaiters { get; private set; } = 0;

    // Class extent (stores all waiter instances privately)
    private static List<Waiter> Waiters = new List<Waiter>();

    // Derived attribute (calculated property, cannot be modified)
    public decimal AverageTipsPerCustomer
    {
        get
        {
            if (averageCustomersServedDaily > 0)
            {
                return avgTipsValuePerDay / averageCustomersServedDaily; // Avoid division by zero
            }
            return 0;
        }
    }

    // Constructor (the only way to set values)
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

        // Increment static count and add to class extent
        TotalWaiters++;
        Waiters.Add(this);
    }

    // Method to display waiter information
    public void DisplayWaiterInfo()
    {
        Console.WriteLine($"Average Tips Value Per Day: {AvgTipsValuePerDay:C}");
        Console.WriteLine($"Average Customers Served Daily: {AverageCustomersServedDaily}");
        Console.WriteLine($"Average Tips Per Customer: {AverageTipsPerCustomer:C}");
    }

    // Static method to display all waiters
    public static void DisplayAllWaiters()
    {
        Console.WriteLine("\n--- All Waiters ---");
        foreach (var waiter in Waiters)
        {
            waiter.DisplayWaiterInfo();
            Console.WriteLine();
        }
    }

    // Serialization method
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

    // Deserialization method
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
