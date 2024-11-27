public class Seating
{
    // Properties of the Seating class
    public int Number { get; set; }            // Unique identifier for the seating (ID)
    public string Type { get; set; }           // Type of seating (e.g., "Booth", "Table", "Bar Stool")
    public bool IfInside { get; set; }         // Whether the seating is located inside

    // Constructor to initialize the Seating class
    public Seating(int number, string type, bool ifInside)
    {
        Number = number;
        Type = type;
        IfInside = ifInside;
    }

    // Method to display seating information
    public void DisplaySeatingInfo()
    {
        Console.WriteLine($"Seating Number: {Number}");
        Console.WriteLine($"Type: {Type}");
        Console.WriteLine($"Located Inside: {(IfInside ? "Yes" : "No")}");
    }
}
