using System;
using System.Collections.Generic;

public class Dishwasher
{
    // Basic Attribute
    public double AvgCleanedDishesPerMinute { get; set; }  // Average number of dishes cleaned per minute

    // Optional Attribute
    public DateTime? ReprimandFromCooksDate { get; set; }  // Nullable reprimand date

    // MultiValue Attribute
    public List<string> DishwasherModelsWorkedOn { get; set; } = new List<string>(); // List of dishwasher models the person worked on

    // Static Attribute
    public static int TotalDishwashers { get; private set; } = 0; // Total number of Dishwasher instances

    // Class Extent
    private static List<Dishwasher> Dishwashers = new List<Dishwasher>(); // Stores all Dishwasher objects

    // Derived Attribute
    public double AverageDishesPerHour
    {
        get { return AvgCleanedDishesPerMinute * 60; } // Calculate average dishes cleaned per hour
    }

    // Constructor for initializing without reprimand date
    public Dishwasher(double avgCleanedDishesPerMinute)
    {
        AvgCleanedDishesPerMinute = avgCleanedDishesPerMinute;
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

    // Method to display dishwasher information
    public void DisplayDishwasherInfo()
    {
        Console.WriteLine($"Average Cleaned Dishes Per Minute: {AvgCleanedDishesPerMinute}");
        Console.WriteLine($"Average Cleaned Dishes Per Hour: {AverageDishesPerHour}");
        Console.WriteLine($"Reprimand From Cooks Date: {ReprimandFromCooksDate?.ToString("d") ?? "None"}");
        Console.WriteLine("Dishwasher Models Worked On: " + (DishwasherModelsWorkedOn.Count > 0 ? string.Join(", ", DishwasherModelsWorkedOn) : "None"));
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
}
