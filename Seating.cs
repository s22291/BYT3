using System;
using System.Collections.Generic;

public class Seating
{
    // Basic Attribute
    public int Number { get; set; } // Unique identifier for the seating (ID)

    // Optional Attribute
    public string SpecialNote { get; set; } // Optional note about the seating (e.g., "Reserved for VIPs")

    // MultiValue Attribute
    public List<string> AvailableForEvents { get; set; } = new List<string>(); // List of events the seating is available for

    // Static Attribute
    public static int TotalSeatings { get; private set; } = 0; // Total number of Seating instances created

    // Class Extent
    private static List<Seating> Seatings = new List<Seating>(); // Stores all Seating instances

    // Complex Attribute
    public DateTime LastMaintenanceDate { get; set; } // Date the seating was last maintained

    // Derived Attribute
    public bool NeedsMaintenance
    {
        get
        {
            // Derived: If last maintenance was more than 1 year ago, it needs maintenance
            return (DateTime.Now - LastMaintenanceDate).TotalDays > 365;
        }
    }

    // Constructor
    public Seating(int number, string type, bool ifInside, DateTime lastMaintenanceDate, string specialNote = null)
    {
        Number = number;
        Type = type;
        IfInside = ifInside;
        LastMaintenanceDate = lastMaintenanceDate;
        SpecialNote = specialNote; // Optional attribute

        // Increment static count and add to class extent
        TotalSeatings++;
        Seatings.Add(this);
    }

    // Method to display seating information
    public void DisplaySeatingInfo()
    {
        Console.WriteLine($"Seating Number: {Number}");
        Console.WriteLine($"Type: {Type}");
        Console.WriteLine($"Located Inside: {(IfInside ? "Yes" : "No")}");
        Console.WriteLine($"Last Maintenance Date: {LastMaintenanceDate:yyyy-MM-dd}");
        Console.WriteLine($"Needs Maintenance: {(NeedsMaintenance ? "Yes" : "No")}");
        Console.WriteLine($"Special Note: {SpecialNote ?? "None"}"); // Handles optional attribute
        Console.WriteLine("Available For Events: " + (AvailableForEvents.Count > 0 ? string.Join(", ", AvailableForEvents) : "None"));
    }

    // Static Method to Display All Seatings
    public static void DisplayAllSeatings()
    {
        Console.WriteLine("\n--- All Seatings ---");
        foreach (var seating in Seatings)
        {
            seating.DisplaySeatingInfo();
            Console.WriteLine();
        }
    }
}

