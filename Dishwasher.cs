using System;

public class Dishwasher
{
    // Properties of the Dishwasher class
    public double AvgCleanedDishesPerMinute { get; set; }  // Average number of dishes cleaned per minute
    public DateTime? ReprimandFromCooksDate { get; set; }  // Optional (nullable) date of reprimand from cooks

    // Constructor for initializing without reprimand date
    public Dishwasher(double avgCleanedDishesPerMinute)
    {
        AvgCleanedDishesPerMinute = avgCleanedDishesPerMinute;
        ReprimandFromCooksDate = null; // Default to no reprimand date
    }

    // Constructor for initializing with reprimand date
    public Dishwasher(double avgCleanedDishesPerMinute, DateTime reprimandFromCooksDate)
    {
        AvgCleanedDishesPerMinute = avgCleanedDishesPerMinute;
        ReprimandFromCooksDate = reprimandFromCooksDate;
    }

    // Method to display dishwasher information
    public void DisplayDishwasherInfo()
    {
        Console.WriteLine($"Average Cleaned Dishes Per Minute: {AvgCleanedDishesPerMinute}");
        Console.WriteLine($"Reprimand From Cooks Date: {ReprimandFromCooksDate?.ToString("d") ?? "None"}");
    }
}
