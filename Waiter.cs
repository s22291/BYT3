/** \file     Waiter.cs
*  \author    Aslan,Julio,Mohammad,Wiktor
*  \version   Final
*  \date      2024
*  \bug       No bugs so far
*  \copyright Polish and Japanies Information Technology 
*/

using System;
using System.Collections.Generic;

public class Waiter
{
    // Basic Attribute
    public decimal AvgTipsValuePerDay { get; set; } // Average value of tips per day

    // Customers served daily (MUST be defined to avoid CS0103)
    public int AverageCustomersServedDaily { get; set; }

    // Optional Attribute
    //  Comments 
    public string ShiftType { get; set; } // Optional shift type (e.g., "Morning", "Evening")

    // MultiValue Attribute
    public List<string> FavoriteTables { get; set; } = new List<string>(); // Favorite tables the waiter likes to serve

    // Static Attribute
    public static int TotalWaiters { get; private set; } = 0; // Total number of Waiters

    // Class Extent
    private static List<Waiter> Waiters = new List<Waiter>(); // Stores all Waiter instances

    // Complex Attribute
    public DateTime DateHired { get; set; } // Date the waiter was hired

    // Derived Attribute
    public decimal AverageTipsPerCustomer
    {
        get
        {
            // Ensure AverageCustomersServedDaily is defined and used properly
            if (AverageCustomersServedDaily > 0)
            {
                return AvgTipsValuePerDay / AverageCustomersServedDaily; // Calculate average tips per customer
            }
            return 0; // Avoid division by zero
        }
    }

    // Constructor
    public Waiter(decimal avgTipsValuePerDay, int averageCustomersServedDaily, string shiftType, DateTime dateHired)
    {
        AvgTipsValuePerDay = avgTipsValuePerDay;
        AverageCustomersServedDaily = averageCustomersServedDaily; // Ensure this matches the property name
        ShiftType = shiftType; // Optional attribute
        DateHired = dateHired;

        // Increment static count and add to class extent
        TotalWaiters++;
        Waiters.Add(this);
    }

    // Method to display waiter information
    public void DisplayWaiterInfo()
    {
        Console.WriteLine($"Average Tips Value Per Day: {AvgTipsValuePerDay:C}"); // Format as currency
        Console.WriteLine($"Average Customers Served Daily: {AverageCustomersServedDaily}");
        Console.WriteLine($"Average Tips Per Customer: {AverageTipsPerCustomer:C}"); // Derived attribute
        Console.WriteLine($"Shift Type: {ShiftType ?? "None"}"); // Handles optional attribute
        Console.WriteLine($"Date Hired: {DateHired:yyyy-MM-dd}");
        Console.WriteLine("Favorite Tables: " + (FavoriteTables.Count > 0 ? string.Join(", ", FavoriteTables) : "None"));
    }

    // Static Method to Display All Waiters
    public static void DisplayAllWaiters()
    {
        Console.WriteLine("\n--- All Waiters ---");
        foreach (var waiter in Waiters)
        {
            // Access instance methods and properties
            waiter.DisplayWaiterInfo();
            Console.WriteLine();
        }
    }
}

